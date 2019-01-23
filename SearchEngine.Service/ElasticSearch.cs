using Nest;
using SearchEngine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SearchEngine.Service
{
    public class ElasticSearch
    {
        private ConnectionSettings _settings;
        private ElasticClient _client;
        private string _urlAPI = ReadString("UrlSearchAPI");
        public ElasticSearch()
        {
            _settings = new ConnectionSettings(new Uri(_urlAPI));
            _settings.DefaultIndex("sample");
            _settings.DisableDirectStreaming(true);
            _client = new ElasticClient(_settings);

        }
        public void CreateIndex()
        {
            _client.DeleteIndex(Indices.Index("sample"));
            var indexSettings = _client.IndexExists("sample");
            if (!indexSettings.Exists)
            {
                var createIndexResponse = _client.CreateIndex("sample", c => c
                                                            .Settings(s => s
                                                                //.NumberOfReplicas(0)
                                                                //.RefreshInterval(-1)
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
                                                                                                            "nbsl => nhuận bút số lượng",
                                                                                                            "gtgt => giá trị gia tăng",
                                                                                                            "tncn => thu nhập cá nhân",
                                                                                                            "thu nhập cá nhân => tncn")
                                                                                                 .Tokenizer("vi_tokenizer")
                                                                                                 )
                                                                                .Shingle("single_filter", stf => stf
                                                                                         .MaxShingleSize(20)
                                                                                         .MinShingleSize(2))
                                                                                        )
                                                                    .Analyzers(an => an
                                                                        .Custom("autocomplete", ca => ca
                                                                            .CharFilters("html_strip")
                                                                            .Tokenizer("vi_tokenizer")
                                                                            .Filters("lowercase", "single_filter")//,"icu_folding"
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

        public List<Question> GetAll(int from = 0 , int pageSize = 10)
        {
            List<Question> lstQuestion = new List<Question>();
            Console.InputEncoding = Encoding.Unicode;
            int index = 0;
            var searchResponse = _client.Search<Question>(s => s
                                                            .From(from)
                                                            .Size(pageSize)
                                                            .Query(q => q.MatchAll())
                                                            );
            if (searchResponse.Hits.Count() != 0)
            {
                Console.WriteLine("######## Result ########## ");
                foreach (var item in searchResponse.Hits)
                {
                    index++;

                    Question question = new Question();
                    question.Body = item.Source.Body;
                    question.Total = (int)searchResponse.Total;
                    lstQuestion.Add(question);
                }
            }

            return lstQuestion;
        }
        public List<Question> Search(string text)
        {
            List<Question> lstQuestion = new List<Question>();

            Console.InputEncoding = Encoding.Unicode;
            int index = 0;

            var searchResponse = _client.Search<Question>(s => s
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
                    //Console.WriteLine(index + " : " + item.Source.Body);

                    //Console.WriteLine(index + " Highligh : " + item.Highlights.Values.Select(x => x.Highlights.FirstOrDefault().ToString()).FirstOrDefault());
                }
            }
            return lstQuestion;
        }
        public List<string> AutoComplete(string text, int sizeSearch = 3, int sizeAutoSuggester = 7)
        {
            List<string> lstSuggest = new List<string>();
            Console.InputEncoding = Encoding.Unicode;
            string patternText = text + ".*";
            var searchResponse = _client.Search<Question>(s => s
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
                                                         );
            var resultSuggester = searchResponse.Aggs.Terms("autoComplete");
            if (resultSuggester.Buckets.Count != 0)
            {
                foreach (var item in resultSuggester.Buckets)
                {
                    if (!item.Key.Contains("_"))
                    {
                        if (text != item.Key)
                        {
                            lstSuggest.Add(item.Key);
                        }
                    }

                }
            }else
            {
                List<Question> lstQuestion = new List<Question>();
                lstQuestion = Search(text);
                if(lstQuestion.Count != 0)
                {
                    foreach(var item in lstQuestion)
                    {
                        lstSuggest.Add(item.Body);
                    }
                }
            }

            return lstSuggest;
        }


        public static string ReadString(string key)
        {
            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory +"AppSettings.config";
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode node = doc.SelectSingleNode("AppSettings");
                XmlNodeList prop = node.SelectNodes("add");

                foreach (XmlNode item in prop)
                {
                    var objKey = item.Attributes["key"];
                    var objVal = item.Attributes["value"];
                    if (objKey.Value == key)
                    {
                        return objVal.Value;
                    }
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
    }
}
