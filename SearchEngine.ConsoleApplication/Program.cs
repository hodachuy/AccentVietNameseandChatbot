using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Elasticsearch.Net;
using SearchEngine.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace SearchEngine.ConsoleApplication
{
    class Program
    {

        static void Main(string[] args)
        {
            //CreateIndexSample();
            //Program pr = new Program();
            //pr.getExcelFile().Wait();

            // pr.CreateDataDemo().Wait();

            //Test();
            Console.OutputEncoding = Encoding.UTF8;

            //----- Test -----//
            //Console.WriteLine("Accuary: " + accent.getAccuracy(System.IO.Path.GetFullPath("test.txt")) + "%");

            while (true)
            {
                Console.InputEncoding = Encoding.Unicode;
                Console.WriteLine("Nhap chuoi :");
                string text = Console.ReadLine();
                if (text == "exit")
                {
                    break;
                }

                //Search(text);
                AutoComplete(text);
            }

        }

        public static void AutoComplete(string text, int sizeSearch = 3, int sizeAutoSuggester = 7)
        {
            List<Question> lstQuestion = new List<Question>();
            ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200"));
            settings.DefaultIndex("sample");
            settings.DisableDirectStreaming(true);
            ElasticClient client = new ElasticClient(settings);

            Console.InputEncoding = Encoding.Unicode;
            int index = 0;

            string patternText = text + ".*";

            //PatternTokenizer pattern = new PatternTokenizer();
            //pattern.Pattern = patternText;

            var searchResponse = client.Search<Question>(s => s
                                                             .Size(0)
                                                             .Aggregations(a => a
                                                                .Terms("autoComplete", x => x
                                                                     .Field(f => f.AutoComplete)
                                                                     .Size(sizeAutoSuggester)
                                                                     // .OrderDescending("_count")
                                                                     .Order(o => o.CountDescending())
                                                                     .Include(patternText)
                                                                    )
                                                                )
                                                         //.Query(q => q
                                                         //    .Match(x => x
                                                         //       .Field(f => f.Body)
                                                         //       .Analyzer("vi_analyzer")
                                                         //       .Boost(1.1)
                                                         //       .Query(text)
                                                         //       )
                                                         //)
                                                         //.Highlight(h => h
                                                         //    .Fields(f => f
                                                         //       .Field(m => m.Body)
                                                         //           .FragmentSize(150)
                                                         //           .NumberOfFragments(3)
                                                         //))

                                                         );

            var resultSuggester = searchResponse.Aggs.Terms("autoComplete");
            Console.WriteLine("######## AutoComplete ########## ");
            if (resultSuggester.Buckets.Count != 0)
            {
                foreach (var item in resultSuggester.Buckets)
                {
                    if (!item.Key.Contains("_"))
                    {
                        if (text != item.Key)
                        {
                            Console.WriteLine(item.Key);
                        }

                    }

                }
            }
            else
            {
                Search(text);
                //if (searchResponse.Hits.Count() != 0)
                //{
                //    foreach (var item in searchResponse.Hits)
                //    {
                //        index++;

                //        Question question = new Question();
                //        question.Body = item.Source.Body;
                //        lstQuestion.Add(question);
                //        Console.WriteLine(index + " : " + item.Source.Body);

                //        Console.WriteLine(index + " Highligh : " + item.Highlights.Values.Select(x => x.Highlights.FirstOrDefault().ToString()).FirstOrDefault());
                //    }
                //}
            }
        }
        public static List<Question> Search(string text)
        {
            List<Question> lstQuestion = new List<Question>();
            ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200"));
            settings.DefaultIndex("sample");
            settings.DisableDirectStreaming(true);
            ElasticClient client = new ElasticClient(settings);

            Console.InputEncoding = Encoding.Unicode;
            int index = 0;

            var searchResponse = client.Search<Question>(s => s
                                                             .Query(q => q
                                                                 .Match(x => x
                                                                    .Field(f => f.Body)
                                                                    .Analyzer("vi_analyzer")
                                                                    .Boost(1.1)
                                                                    //.CutoffFrequency(0.001)
                                                                    .Query(text)
                                                                    //.Fuzziness(Fuzziness.Auto)
                                                                    //.Lenient()
                                                                    //.FuzzyTranspositions()
                                                                    //.Operator(Operator.Or)
                                                                    )
                                                             )
                                                             .Highlight(h => h
                                                                 .Fields(f => f
                                                                    .Field(m => m.Body)
                                                                        .FragmentSize(150)
                                                                        .NumberOfFragments(3)
                                                             ))

                                                         );
            if (searchResponse.Hits.Count() != 0)
            {
                Console.WriteLine("######## Result ########## ");
                foreach (var item in searchResponse.Hits)
                {
                    index++;

                    Question question = new Question();
                    question.Body = item.Source.Body;
                    lstQuestion.Add(question);
                    Console.WriteLine(index + " : " + item.Source.Body);

                    Console.WriteLine(index + " Highligh : " + item.Highlights.Values.Select(x => x.Highlights.FirstOrDefault().ToString()).FirstOrDefault());
                }
            }
            return lstQuestion;
        }
        public static void CreateIndexSample()
        {
            ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200"));
            settings.DefaultIndex("sample");
            ElasticClient client = new ElasticClient(settings);
            client.DeleteIndex(Indices.Index("sample"));
            var indexSettings = client.IndexExists("sample");
            if (!indexSettings.Exists)
            {
                //var indexName = new IndexName();
                //indexName.Name = "sample";

                var createIndexResponse = client.CreateIndex("sample", c => c
                                                            .Settings(s => s
                                                                .NumberOfShards(1)
                                                                .Analysis(a => a
                                                                    .TokenFilters(tf => tf
                                                                                .Stop("stop", x => x
                                                                                             .StopWords("bị", "bởi", "cả", "các", "cái", "cần", "càng", "chỉ", "chiếc", "cho", "chứ", "chưa", "chuyện",
                                                                                             "có", "có thể", "cứ", "của", "cùng", "cũng", "đã", "đang", "đây", "để", "đến nỗi", "đều", "điều",
                                                                                             "do", "đó", "được", "dưới", "gì", "khi", "không", "là", "lại", "lên", "lúc", "mà", "mỗi", "một cách",
                                                                                             "này", "nên", "nếu", "ngay", "nhiều", "như", "nhưng", "những", "nơi", "nữa", "phải", "qua", "ra",
                                                                                             "rằng", "rằng", "rất", "rất", "rồi", "sau", "sẽ", "so", "sự", "tại", "theo", "thì", "trên", "trước",
                                                                                             "từ", "từng", "và", "vẫn", "vào", "vậy", "vì", "việc", "với", "vừa"))
                                                                                .SynonymGraph("synonym_filter", m => m
                                                                                                 .Synonyms("nbcb => nhuận bút cơ bản",
                                                                                                            "nbsl => nhuận bút số lượng")
                                                                                                 .Tokenizer("vi_tokenizer"))
                                                                                .Shingle("single_filter", stf => stf
                                                                                         .MaxShingleSize(20)
                                                                                         .MinShingleSize(2))
                                                                                        )
                                                                    .Analyzers(an => an
                                                                        .Custom("autocomplete", ca => ca
                                                                            .CharFilters("html_strip")
                                                                            .Tokenizer("vi_tokenizer")
                                                                            .Filters("lowercase", "single_filter")//,"icu_folding","single_filter"
                                                                        )
                                                                        .Custom("vi_analyzer", ca => ca
                                                                            .CharFilters("html_strip")
                                                                            .Tokenizer("vi_tokenizer")
                                                                            .Filters("lowercase", "stop", "synonym_filter", "icu_folding")//,"icu_folding"
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                            .Mappings(m => m
                                                                .Map<Question>(mm => mm
                                                                    .AutoMap()
                                                                    .Properties(p => p
                                                                        .Text(z => z
                                                                            .Name(g => g.AutoComplete)
                                                                            .Analyzer("autocomplete")
                                                                            .Fielddata(true)
                                                                            )
                                                                        .Text(t => t
                                                                            .Name(n => n.Body)
                                                                            .Analyzer("vi_analyzer")
                                                                            .CopyTo(x => x
                                                                            .Fields(y => y.AutoComplete))
                                                                       )
                                                                    )
                                                                )
                                                            )
                                                        );


            }

            if (indexSettings.Exists)
            {
                Console.WriteLine("Created");
            }
        }
        public static void Test()
        {
            ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200"));
            ElasticClient client = new ElasticClient(settings);
            var analyzeResponse = client.Analyze(a => a

                .Analyzer("vi_analyzer")
                //.Tokenizer("vi_tokenizer")
                .Text("nước hoa có khác nhau giữa người này với người khác")
                .Filter("lowercase")

            );
            Console.OutputEncoding = UTF8Encoding.UTF8;
            foreach (var analyzeToken in analyzeResponse.Tokens)
            {
                Console.WriteLine($"{analyzeToken.Token}");
            }
        }

        public async Task CreateDataDemo()
        {
            ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200"));

            settings.DefaultIndex("sample");
            ElasticClient esClient = new ElasticClient(settings);

            List<Question> _lstQuestion = new List<Question>();
            _lstQuestion.Add(new Question { Id = 1, Body = "Huy là sinh viên đại học", Score = 1, CreationDate = DateTime.Now });
            _lstQuestion.Add(new Question { Id = 2, Body = "Tèo đang học mỹ thuật", Score = 1, CreationDate = DateTime.Now });
            _lstQuestion.Add(new Question { Id = 3, Body = "Tí đang đi du lịch tại Mỹ Tho", Score = 1, CreationDate = DateTime.Now });
            _lstQuestion.Add(new Question { Id = 4, Body = "Tú đang du học ở Mỹ", Score = 1, CreationDate = DateTime.Now });
            _lstQuestion.Add(new Question { Id = 5, Body = "VN đang đá bóng ở sân Mỹ Đình", Score = 1, CreationDate = DateTime.Now });
            _lstQuestion.Add(new Question { Id = 6, Body = "bạn Đại học ở Mỹ Tho", Score = 1, CreationDate = DateTime.Now });
            _lstQuestion.Add(new Question { Id = 7, Body = "bạn học tên Nguyễn Văn Mỹ", Score = 1, CreationDate = DateTime.Now });



            _lstQuestion.Add(new Question { Id = 8, Body = "Làm thế nào bạn phân biệt được mùi dầu thơm ban đầu, hương chính, hương nền", Score = 1, CreationDate = DateTime.Now });
            _lstQuestion.Add(new Question { Id = 9, Body = "Nước hoa có khác nhau giữa người này với người khác không? Vì sao?", Score = 1, CreationDate = DateTime.Now });

            foreach (var item in _lstQuestion)
            {
                await esClient.IndexAsync<Question>(item, i => i
                                              .Index("sample")
                                              .Type(TypeName.From<Question>())
                                              .Id(item.Id)
                                              .Refresh(Elasticsearch.Net.Refresh.True));
            }
        }

        public async Task getExcelFile()
        {
            ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200"));

            settings.DefaultIndex("sample");
            ElasticClient esClient = new ElasticClient(settings);

            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"D:\HDHUY-DATA\Hoi-dap ve quyen tac gia.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            Console.OutputEncoding = UTF8Encoding.UTF8;
            for (int i = 2; i <= rowCount; i++)
            {
                int j = 2;
                //for (int j = 2; j <= colCount; j++)
                //{
                ////new line
                //if (j == 1)
                //    Console.Write("\r\n");

                //write the value to the console
                if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                {
                    Console.Write(xlRange.Cells[i, 1].Value2.ToString() + "\t" + xlRange.Cells[i, j].Value2.ToString() + "\r\n");

                    Question question = new Question();
                    question.Id = i;
                    question.Score = 1;
                    question.CreationDate = DateTime.Now;
                    question.Body = xlRange.Cells[i, j].Value2.ToString();
                    //question.AutoComplete = xlRange.Cells[i, j].Value2.ToString();
                    await esClient.IndexAsync<Question>(question, x => x
                                          .Index("sample")
                                          .Type(TypeName.From<Question>())
                                          .Id(question.Id)
                                          .Refresh(Elasticsearch.Net.Refresh.True));
                }

                //}
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }


    }
}
