$(document).ready(function () { var ticker_content = $('#ticker').html(); var ticker_righttoleft = false; var ticker_speed = 2; var ticker_style = "font-size:18pt; color:black; font-family: 'PT Sans', sans-serif;"; var ticker_paused = false; ticker_start(); function ticker_start() { var tickerSupported = false; var ticker_width = $('#ticker').width(); var img = "<img src='../../Content/Images/ticker_space.gif' width=" + ticker_width + " height=0>"; if ($.browser.mozilla || $.browser.netscape || $.browser.chrome || $.browser.safari) { var text = "<table cellspacing='0' cellpadding='0' width='100%'><tr><td nowrap='nowrap'>" + img + "<span style='" + ticker_style + "' id='ticker_body' width='100%'>&nbsp;</span>" + img + "</td></tr></table>"; $('#ticker').html(text); tickerSupported = true } if ($.browser.msie || $.browser.opera) { var text = "<div nowrap='nowrap' style='width:100%;'>" + img + "<span style='" + ticker_style + "' id='ticker_body' width='100%'></span>" + img + "</div>"; $('#ticker').html(text); tickerSupported = true } if (!tickerSupported) $('#ticker').outerHTML(""); else { var newScrollLeft = ticker_righttoleft ? $('#ticker').attr("scrollWidth") - $('#ticker').attr('offsetWidth') : 0; $('#ticker').scrollLeft(newScrollLeft); $("#ticker_body").html(ticker_content); $('#ticker').css("display", "block"); ticker_tick() } } function ticker_tick() { var scrollLeft = $('#ticker').scrollLeft(); if (!ticker_paused) { var newScrollLeft = scrollLeft + ticker_speed * (ticker_righttoleft ? -1 : 1); $('#ticker').scrollLeft(newScrollLeft) } if (ticker_righttoleft && $('#ticker').scrollLeft() <= 0) { var newScrollLeft = $('#ticker').attr("scrollWidth") - $('#ticker').attr('offsetWidth'); $('#ticker').scrollLeft(newScrollLeft) } if (!ticker_righttoleft && $('#ticker').scrollLeft() >= $('#ticker').attr("scrollWidth") - $('#ticker').attr('offsetWidth')) { $('#ticker').scrollLeft(0) } window.setTimeout(ticker_tick, 30) } jQuery.fn.outerHTML = function (s) { return (s) ? this.before(s).remove() : jQuery("&lt;p&gt;").append(this.eq(0).clone()).html() } });