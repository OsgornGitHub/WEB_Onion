var pageNum = 1;
var count = 24;
var isSimilar = false;
console.log(pageNum);

function getJson(page, count) {
    $.ajax({
        type: "GET",
        url: "Home/GetTopArtistJson",
        data: { page: pageNum, count: count },
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
            <a ` + similar + ` href="http://172.19.0.251:45455/Home/GetArtist?name=${data[i].name}">
                <div class="col-md-2">
                    <img src="${data[i].photo}" style="width: 100%" />
                    <h4 class="text-center">${data[i].name}</h4>
                </div>
            </a>
            `;
            container.append(markup);
        }
    }
    //window.history.pushState("http://172.19.0.251:45455/Home/Index/?page=", "Index", "http://172.19.0.251:45455/Home/Index/?page=" + pageNum);
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
    //var div_1 = document.getElementById('page_prev');
    //var div_2 = document.getElementById('page_next');
    //console.log(pageNum);

    console.log(pageNum);
    div.innerHTML = pageNum + "";
    //if (pageNum != 1) {
    //    div_1.innerHTML = pageNum - 1 + "";
    //}
    //div_2.innerHTML = pageNum + 1 + "";


    //$('.go').click(function () {
    //    var input = document.getElementById('input').value;
    //    pageNum = input;
    //    getJson(pageNum, count);
    //    var div = document.getElementById('page');
    //    div.innerHTML = pageNum + "";
    //    //div_1.innerHTML = pageNum - 1 + "";
    //    //div_2.innerHTML = pageNum + 1 + "";
    //    console.log(pageNum);
    //});

    $('.next').click(function () {
        isSimilar = false;
        pageNum++;
        getJson(pageNum, count);
        var div = document.getElementById('page');
        div.innerHTML = pageNum + "";
        //div_1.innerHTML = pageNum - 1 + "";
        //div_2.innerHTML = pageNum + 1 + "";
        console.log(pageNum);
    })


    $('.previous').click(function () {
        isSimilar = false;
        if (pageNum !== 1) {
            pageNum--;
            getJson(pageNum, count);
            var div = document.getElementById('page');
            div.innerHTML = pageNum + "";
            //if (pageNum != 1) {
            //    div_1.innerHTML = pageNum - 1 + "";
            //}
            //div_2.innerHTML = pageNum + 1 + "";

            console.log(pageNum);
        }
    })

    $('.similar').click(function () {
        var name = document.getElementById('name').innerText;
        isSimilar = true;
        getSimilar(name)
        console.log(name);
    })


    $('.12').click(function () {
        isSimilar = false;
        pageNum = 1;
        count = 12;
        getJson(pageNum, count);
        div.innerHTML = pageNum + "";
        //if (pageNum != 1) {
        //    div_1.innerHTML = pageNum - 1 + "";
        //}
        //div_2.innerHTML = pageNum + 1 + "";
    })

    $('.24').click(function () {
        isSimilar = false;
        pageNum = 1;
        count = 24;
        getJson(pageNum, count);
        div.innerHTML = pageNum + "";
        //if (pageNum != 1) {
        //    div_1.innerHTML = pageNum - 1 + "";
        //}
        //div_2.innerHTML = pageNum + 1 + "";
    })

    $('.36').click(function () {
        isSimilar = false;
        pageNum = 1;
        count = 36;
        getJson(pageNum, count);
        div.innerHTML = pageNum + "";
        //if (pageNum != 1) {
        //    div_1.innerHTML = pageNum - 1 + "";
        //}
        //div_2.innerHTML = pageNum + 1 + "";
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
