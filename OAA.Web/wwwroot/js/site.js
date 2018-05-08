var pageNum = 1;
var count = 24;
var totalPage = 416;
var isSimilar = false;
console.log(pageNum);


$(function () {
    window.pagObj = $('#pagination').twbsPagination({
        totalPages: totalPage,
        visiblePages: 10,
        onPageClick: function (event, page) {
            console.info(page);
            window.history.pushState("", "Index", "?page=" + page);
        }
    }).on('page', function (event, page) {
        console.log(page);
        getTopArtistJson(page, count)

    });
});


function getTopArtistJson(page, count) {
    $.ajax({
        type: "GET",
        url: "Home/GetTopArtistJson",
        data: { page: page, count: count },
        dataType: "json",
        success: function (data) { loadData(data); }
    });
}

function loadData(data) {
    console.log(data)
    var container = $('div.artist');

    if (isSimilar) {
        similar = `target="_blank"`;
    }
    else {
        similar = "";
    }

    container.html('');
    if (data !== -1) {
        for (var i = 0; i < data.length; i++) {
            var markup =
                `
            <a ` + similar + ` href="Home/GetArtist?name=${data[i].name}">
                <div class="col-md-2">
                    <img src="${data[i].photo}" style="width: 100%" />
                    <h4 class="text-center">${data[i].name}</h4>
                </div>
            </a>
            `;
            container.append(markup);
        }
    }
}


$(document).ready(function () {
    var div = document.getElementById('page');

    //var audio = document.getElementById("audio");
    //function control() {
    //    document.addEventListener('keydown', function (e) {
    //        if (!audio.paused) audio.pause();
    //        if (e.which == 39) {
    //            audio.currentTime += 10;
    //        }
    //        if (e.which == 37) {
    //            audio.currentTime -= 10;
    //        }
    //    }, false);
    //    document.addEventListener('keyup', function (e) {
    //        if ((e.which == 39 || e.which == 37) && audio.paused) {
    //            audio.play();
    //        }
    //    }, false);
    //}

    document.addEventListener('play', function (e) {
        var audios = document.getElementsByTagName('audio');
        localStorage['audio'] += audios;
        for (var i = 0; i < audios.length; i++) {
            if (audios[i] != e.target) {
                audios[i].pause();
            }
        }

    }, true);



    window.addEventListener('storage', storageEventHandler, false);

    function storageEventHandler(e) {
        console.log(e.key); //имя
        var audios = document.getElementsByTagName('audio');
        for (var i = 0; i < audios.length; i++) {
            if (audios[i] != e.target) {
                audios[i].pause();
            }
        }
    }


    $('.12').click(function () {
        isSimilar = false;
        page = 1;
        count = 12;
        var val = 'First';
        $('a:contains("' + val + '")').get(0).click();
        $('a:contains("' + val + '")').addClass('on');

    })

    $('.24').click(function () {
        isSimilar = false;
        page = 1;
        count = 24;
        var val = 'First';
        $('a:contains("' + val + '")').get(0).click();
        $('a:contains("' + val + '")').addClass('on');


    })

    $('.36').click(function () {
        isSimilar = false;
        page = 1;
        count = 36;
        var val = 'First';
        $('a:contains("' + val + '")').get(0).click();
        $('a:contains("' + val + '")').addClass('on');


    })

    $('.add').click(function () {
        var id = $(this).attr('id');
        console.log(id);
        $('.' + id).toggleClass('invis');
    })



});
