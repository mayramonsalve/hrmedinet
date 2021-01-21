$("#MultiPageAnswers").live('submit', function (e) {
    var val = $('#MultiPageAnswers').validate();
    val.showErrors();
    alert(val.valid());
    e.preventDefault();
});