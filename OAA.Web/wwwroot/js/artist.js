$(document).ready(function () {
    $('.div_artist').css("display", "block")

    $('.artist').click(function () {
        $('.div_artist').css("display","block")

        $('.div_tracks').css("display", "none")
        $('.div_albums').css("display", "none")
        $('.div_similar').css("display", "none")
        console.log("ok1");
    });

    $('.tracks').click(function () {
        $('.div_tracks').css("display", "block")

        $('.div_artist').css("display", "none")
        $('.div_albums').css("display", "none")
        $('.div_similar').css("display", "none")

        console.log("ok2")
    });

    $('.albums').click(function () {
        $('.div_albums').css("display", "block")

        $('.div_artist').css("display", "none")
        $('.div_tracks').css("display", "none")
        $('.div_similar').css("display", "none")

        console.log("ok3")

    });

    $('.similar').click(function () {
        $('.div_similar').css("display", "block")

        $('.div_artist').css("display", "none")
        $('.div_tracks').css("display", "none")
        $('.div_albums').css("display", "none")

        console.log("ok4")

    });

});
