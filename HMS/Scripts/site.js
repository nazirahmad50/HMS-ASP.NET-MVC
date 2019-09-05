//TODO: create seperate script file in Scripts folder and add it to _layout and do this for all scripts that are used in side a view

$(".changeAccomadationtype").click(function () {

    // get the value from attribuete 'data-id' based on which button is clicked
    var accomadationTypes = $(this).attr("data-id");

    $(".accomadationtypesRow").hide(); // hide class accomadationtypesRow

    // add the 'accomadationTypes' to attribute 'data-id' of class 'accomadationtypesRow' and show it
    $(".accomadationtypesRow[data-id=" + accomadationTypes + "]").show();


});

