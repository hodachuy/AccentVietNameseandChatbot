/*
    Hiển thị khoản thời gian so với thời gian hiện tại
    Ví dụ:
    Input:
    <abbr class="timeago" title="2020-04-17T09:24:17Z">2011-12-17T09:24:17Z1</abbr>
    <abbr class="timeago" title="December 17, 2015">December 17, 201o</abbr>
    <time class="timeago" datetime="2016-01-17T09:24:17Z">2013-01-17T09:24:17Z</time>
    <span class="timeago" title="1372407476">1372407476</span>
    Output:
    javascript, timeago, time, date
    about a month ago
    4 years ago
    4 years ago
    7 years ago
*/
function getTimeAgo(selector) {

    var templates = {
        prefix: "",
        suffix: " trước",
        seconds: "Hiện tại",
        minute: "1 phút",
        minutes: "%d phút",
        hour: "1 giờ",
        hours: "%d giờ",
        day: "Hôm qua",
        days: "%d ngày",
        month: "1 tháng",
        months: "%d tháng",
        year: "1 năm",
        years: "%d năm"
    };
    //prefix: "",
    //suffix: " ago",
    //seconds: "less than a minute",
    //minute: "about a minute",
    //minutes: "%d minutes",
    //hour: "about an hour",
    //hours: "about %d hours",
    //day: "a day",
    //days: "%d days",
    //month: "about a month",
    //months: "%d months",
    //year: "about a year",
    //years: "%d years"
    var template = function (t, n) {
        return templates[t] && templates[t].replace(/%d/i, Math.abs(Math.round(n)));
    };

    var timer = function (time) {
        if (!time) return;
        time = time.replace(/\.\d+/, ""); // remove milliseconds
        time = time.replace(/-/, "/").replace(/-/, "/");
        time = time.replace(/T/, " ").replace(/Z/, " UTC");
        time = time.replace(/([\+\-]\d\d)\:?(\d\d)/, " $1$2"); // -04:00 -> -0400
        time = new Date(time * 1000 || time);

        var now = new Date();
        var seconds = ((now.getTime() - time) * .001) >> 0;
        var minutes = seconds / 60;
        var hours = minutes / 60;
        var days = hours / 24;
        var years = days / 365;

        return templates.prefix + (
        seconds < 45 && template('seconds', seconds) || seconds < 90 && template('minute', 1) || minutes < 45 && template('minutes', minutes) || minutes < 90 && template('hour', 1) || hours < 24 && template('hours', hours) || hours < 42 && template('day', 1) || days < 30 && template('days', days) || days < 45 && template('month', 1) || days < 365 && template('months', days / 30) || years < 1.5 && template('year', 1) || template('years', years)); //+templates.suffix
    };

    var elements = document.getElementsByClassName('timeago');
    for (var i in elements) {
        var $this = elements[i];
        if (typeof $this === 'object') {
            $this.innerHTML = timer($this.getAttribute('title') || $this.getAttribute('datetime'));
        }
    }
    // update time every minute
    setTimeout(getTimeAgo, 60000);

};