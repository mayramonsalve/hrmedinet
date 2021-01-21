<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    MapUS
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Global climate</h2>

    <div id="contenido-sistema" class="span-24 column last">
    <div class="clear">&nbsp;</div>
        <div id="map" style="height:500px;">

        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="../../Content/Css/jquery.vector-map.css" media="screen" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="../../Scripts/css3-mediaqueries.js" type="text/javascript"></script>
    <script src="../../Scripts/modernizr.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.vector-map.js" type="text/javascript"></script>
    <script src="../../Scripts/world-en.js" type="text/javascript"></script>
    <script src="../../Scripts/ve-en.js" type="text/javascript"></script>
    <script src="../../Scripts/us-en.js" type="text/javascript"></script>
    <script src="../../Scripts/ar-en.js" type="text/javascript"></script>
    <script type="text/javascript">


        $(function () {
            var green = "#32CD32"; /*32CD32*/
            var amber = "#FDEE00";
            var red = "#FF4000";
            var states = new Array("VE-A", "VE-B", "VE-C", "VE-D", "VE-E", "VE-F", "VE-G", "VE-H", "VE-I", "VE-J", "VE-K", "VE-L", "VE-M", "VE-N",
                                    "VE-O", "VE-P", "VE-R", "VE-S", "VE-T", "VE-U", "VE-V", "VE-X", "VE-Y", "VE-Z");
            var climate = new Array(40, 55, 60, 78, 89, 98, 90, 76, 77, 89, 45, 67, 87, 80, 70, 90, 46, 86, 84, 85, 60, 89, 95, 96);
            $('#map').vectorMap({
                map: 've-en',
                //colors: changeColors(states,climate),
                color: '#cccccc',
                backgroundColor: false,
                hoverOpacity: 0.7,
                hoverColor: false
            });
            //            $('#map').vectorMap('set', 'colors', { VE-L: red });
            //            $('#map').vectorMap('set', 'colors', { VE-M: green });
            //            $('#map').vectorMap('set', 'colors', { VE-N: amber });
            //            $('#map').vectorMap('set', 'colors', { VE-O: green });
            //            $('#map').vectorMap('set', 'colors', { VE-H: amber });
            //            $('#map').vectorMap('set', 'colors', { VE-I: red });
            //            $('#map').vectorMap('set', 'colors', { VE-J: amber });
            //            $('#map').vectorMap('set', 'colors', { VE-K: green });
            //            $('#map').vectorMap('set', 'colors', { VE-D: green });
            //            $('#map').vectorMap('set', 'colors', { VE-E: green });
            //            $('#map').vectorMap('set', 'colors', { VE-F: red });
            //            $('#map').vectorMap('set', 'colors', { VE-G: amber });
            //            $('#map').vectorMap('set', 'colors', { VE-A: green });
            //            $('#map').vectorMap('set', 'colors', { VE-B: red });
            $('#map').vectorMap('set', 'colors', changeColors(states, climate));
            function changeColors(states, pctClimate) {
                var newColors = {};
                var blue = "#0066FF";
                var green = "#66FF00";
                var amber = "#FFFF00";
                var orange = "#FF8C00";
                var red = "#FF0000";
                var gray = "#CCCCCC";
                var hex = "";
                for (var pos = 0; pos < states.length; pos++) {
                    var st = states[pos];
                    if (pctClimate[pos] <= 60)
                        hex = red;
                    else if (pctClimate[pos] > 60 && pctClimate[pos] <= 80)
                        hex = amber;
                    else if (pctClimate[pos] > 80)
                        hex = green;
                    else hex = gray;
                    newColors[st] = hex;
                }
                return newColors;
            }
        });
    </script>
</asp:Content>
