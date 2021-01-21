$(function () {
    //    jQuery("#myTabs").delegate(".textEditor", "change", function () {
    //        
    //    });

    jQuery("#myTabs").delegate(".textEditor", { "tinymice": function () {
        script_url: '../../Scripts/tiny_mce.js';
        theme: "advanced";
        theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,fontselect,fontsizeselect";
        theme_advanced_buttons2: "bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,forecolor,backcolor";
        theme_advanced_toolbar_location: "top";
        theme_advanced_toolbar_align: "left";
        theme_advanced_statusbar_location: "none";
        theme_advanced_resizing: false;
    }
    });

    //    $(".textEditor").tinymce({
    //        script_url: '../../Scripts/tiny_mce.js',
    //        theme: "advanced",
    //        theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,fontselect,fontsizeselect",
    //        theme_advanced_buttons2: "bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,forecolor,backcolor",
    //        theme_advanced_toolbar_location: "top",
    //        theme_advanced_toolbar_align: "left",
    //        theme_advanced_statusbar_location: "none",
    //        theme_advanced_resizing: false
    //    });
});