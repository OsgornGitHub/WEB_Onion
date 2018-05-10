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
        $('#artist').load('Home/GetPage?page=' + page + "&count=" + count);

    });
});





$(document).ready(function () {
    var div = document.getElementById('page');
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
