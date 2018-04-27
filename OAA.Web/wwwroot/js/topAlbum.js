var name = "";
var pageAlbum;
var countAlbum;


function getAlbums(name, pageAlbum, countAlbum) {
    $.ajax({
        type: "GET",
        url: "/Home/GetTopAlbum",
        data: { name: name, page: pageAlbum, count: countAlbum },
        dataType: "json",
        success: function (data) { loadAlbums(data); }
    });
}

function loadAlbums(data) {
    console.log(data)
    var container = $('div.albums');
    container.html('');
    if (data !== -1) {
        for (var i = 0; i < data.length; i++) {
            var markup = `
               <a  href="/Home/GetAlbum?nameAlbum=${data[i].nameAlbum}&nameArtist=${data[i].nameArtist}">
                    <div class="col-md-2" style="height: 350px">
                        <img src="${data[i].cover}" style="width: 100%" />
                        <h4 class="text-center" style="margin-bottom: 15px">${data[i].nameAlbum}</h4>
                    </div> 
               </a>
            `;
            container.append(markup);
        }
    }
}


$(document).ready(function () {
    name = document.getElementById('name').innerText;
    console.log(name);
    $('.top_albums').click(function () {
        pageAlbum = 1;
        countAlbum = 24;
        getAlbums(name, pageAlbum, countAlbum);
        console.log(name);
    })

});
