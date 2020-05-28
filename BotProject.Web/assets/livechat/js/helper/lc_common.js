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


//Get Map Device IP
function initLatiLongMap(latitude, longitude) {
    var posVietNam = { lat: 16.4498, lng: 107.5624 };
    var zoomSize = 5;
    if (latitude && longitude) {
        posVietNam = { lat: parseFloat(latitude), lng: parseFloat(longitude) };
        zoomSize = 9;
    }
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: zoomSize,
        center: posVietNam,
        mapTypeControl: false,
        zoomControl: false,
        scaleControl: false,
        streetViewControl: false,
        rotateControl: false,
        fullscreenControl: false
    });
    marker = new google.maps.Marker({
        map: map,
        draggable: false,
        animation: google.maps.Animation.DROP,
        position: posVietNam
    });
    google.maps.event.addListener(marker, 'dragend', function (event) {
        document.getElementById("Latitude").value = this.getPosition().lat();
        document.getElementById("Longitude").value = this.getPosition().lng();
    });
    //autocomplete = new google.maps.places.Autocomplete(
    //    (document.getElementById('address')), { types: ['geocode'] });
    var input = document.getElementById('Address');
    autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.addListener('place_changed', fillInAddress);
}

function fillInAddress() {
    // Get the place details from the autocomplete object.
    var place = autocomplete.getPlace();
    document.getElementById("LatiLongTude").value = place.geometry.location.lat() + "," + place.geometry.location.lng();
    var pos = { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() };
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 9,
        center: pos,
        mapTypeControl: false,
        zoomControl: false,
        scaleControl: false,
        streetViewControl: false,
        rotateControl: false,
        fullscreenControl: false
    }); var image = {
        url: "/assets/client/img/gmap_marker.png",
        anchor: new google.maps.Point(25, 25),
        scaledSize: new google.maps.Size(45, 45)
    };
    marker = new google.maps.Marker({
        map: map,
        draggable: false,
        animation: google.maps.Animation.DROP,
        position: pos,
        //icon: image
    });
    google.maps.event.addListener(marker, 'dragend', function (event) {
        document.getElementById("LatiLongTude").value = this.getPosition().lat() + "," + this.getPosition().lng();
    });
}

function getLatitudeLongitude(callback, address) {
    address = address || 'Ferrol, Galicia, Spain';
    geocoder = new google.maps.Geocoder();
    if (geocoder) {
        geocoder.geocode({
            'address': address
        }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                callback(results[0]);
            }
        });
    }
}


/*
    Ký tự icon tới image icon
*/
var iconFace = {
    smiley: ':smiley:',//:)
    smile: ':smile:',//^_^,^-^
    grinning: ':grinning:',//:D
    open_mouth: ':open_mouth:',//:o,:0,:O
    disappointed: ':disappointed:',//:(
    expressionless: ':expressionless:',//-_-
    grin: ':grin:',//:v
    heart: ':heart:',//<3 "&lt;3"
    like: ':+1:',//(y)
    confounded: ':confounded:',   //:3
    stuck_out_tongue: ':stuck_out_tongue:'//:P
}
function isValidURLandCodeIcon(string) {
    var res = string.match(/(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/g);
    if (res != null) {
        string = string.replace(res, "<a href='" + res + "' target='_blank' style='text-decoration:underline'>" + res + "</a>");
    }
    else if (string.includes(":)H")) {
        string = string.replace(':)H', '<img style="display:inline-block;width:25px;height:25px;border-radius:30px" src="' + _Host + 'assets/client/libs/emoji-picker/img/me.jpg" alt="" />');
    }
    else if (string.includes(":)")) {
        string = string.replace(':)', returnIcon(iconFace.smiley, -25, 0));
    }
    else if (string.includes("^-^") || string.includes("^_^")) {
        string = string.replace('^-^', returnIcon(iconFace.smile, 0, 0));
        string = string.replace('^_^', returnIcon(iconFace.smile, 0, 0));
    }
    else if (string.includes(":D")) {
        string = string.replace(':D', returnIcon(iconFace.grinning, -50, 0));
    }
    else if (string.includes(":o") || string.includes(":0") || string.includes(":O")) {
        string = string.replace(':o', returnIcon(iconFace.open_mouth, -550, 25));
        string = string.replace(':0', returnIcon(iconFace.open_mouth, -550, 25));
        string = string.replace(':O', returnIcon(iconFace.open_mouth, -550, 25));
    }
    else if (string.includes(":(")) {
        string = string.replace(':(', returnIcon(iconFace.disappointed, -475, 0));
    }
    else if (string.includes("-_-")) {
        string = string.replace('-_-', returnIcon(iconFace.expressionless, -75, -50));
    }
    else if (string.includes(":v")) {
        string = string.replace(':v', returnIcon(iconFace.grin, -375, 0));
    }
    else if (string.includes("&lt;3")) {
        string = string.replace('&lt;3', returnIcon(iconFace.heart, -250, -150));
    }
    else if (string.includes("(y)")) {
        string = string.replace('(y)', returnIcon(iconFace.like, -600, -75));
    }
    else if (string.includes(":3")) {
        string = string.replace(':3', returnIcon(iconFace.confounded, -225, -25));
    }
    else if (string.includes(":P")) {
        string = string.replace(':P', returnIcon(iconFace.stuck_out_tongue, -325, 0));
    }
    return string;
};
function returnIcon(code, positionX, positionY) {
    var imageIcon = '<img src="' + _Host + 'assets/client/libs/emoji-picker/img/blank.gif" class="img" style="display:inline-block;width:25px;height:25px;background:url(' + "'assets/client/libs/emoji-picker/img/emoji_spritesheet_0.png'" + ') ' + positionX + 'px ' + positionY + 'px no-repeat;background-size:675px 175px;" alt="' + code + '">';
    return imageIcon
}


/*
* Get time chat current
*/
function showTimeChat() {
    date = new Date();
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return strTime;
}