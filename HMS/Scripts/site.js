//TODO: create seperate script file in Scripts folder and add it to _layout and do this for all scripts that are used in side a view

$(".changeAccomadationtype").click(function () {

    var accomadationTypes = $(this).attr("data-id");

    $(".accomadationtypesRow").hide();
    $(".accomadationtypesRow[data-id=" + accomadationTypes + "]").show();


});

