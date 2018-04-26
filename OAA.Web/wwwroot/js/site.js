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
            console.info(page + ' (from options)');
            window.history.pushState("", "Index", "?page=" + page);
        }
    }).on('page', function (event, page) {
        console.log(page);
        getTopArtistJson(page, count)
    });
});


function getCountPageTopArtist(count) {
    $.ajax({
        type: "GET",
        url: "Home/GetCountPageTopArtist",
        data: { page: 1, count: count },
        dataType: "json",
        success: function (data) { return data }
    });
}

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
            <a ` + similar + ` href="/Home/GetArtist?name=${data[i].name}">
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

function getSimilar(name) {
    $.ajax({
        type: "GET",
        url: "/Home/GetListSimilar",
        data: { name: name },
        dataType: "json",
        success: function (data) { loadData(data, isSimilar); }
    });
}


$(document).ready(function () {
    var div = document.getElementById('page');

    $('.similar').click(function () {
        var name = document.getElementById('name').innerText;
        isSimilar = true;
        getSimilar(name)
        console.log(name);
    })


    $('.12').click(function () {
        isSimilar = false;
        page = 1;
        count = 12;
        getTopArtistJson(pageNum, count);
        div.innerHTML = pageNum + "";

    })

    $('.24').click(function () {
        isSimilar = false;
        page = 1;
        count = 24;
        getTopArtistJson(pageNum, count);
        div.innerHTML = pageNum + "";

    })

    $('.36').click(function () {
        isSimilar = false;
        page = 1;
        count = 36;
        getTopArtistJson(pageNum, count);
        div.innerHTML = pageNum + "";

    })

    $('.sim').click(function () {
        $('.nava').addClass('visible');
    })

    $('.alb').click(function () {
        $('.nava').addClass('visible');
    })

    $('.tr').click(function () {
        $('.nava').addClass('visible');
    })


});
