<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<link href="/Content/Css/style.css" rel="stylesheet" type="text/css" />
<link href="/Content/Css/nivo-slider.css" rel="stylesheet" type="text/css" media="screen" />
<link href="/Content/Css/default.css" rel="stylesheet" type="text/css" media="screen" />

<div id="slider" class="nivoSlider">
    <%--<ul id="slider1">
            <li><img src= "<%: ViewRes.Views.Home.Index.AnalysisImage %>" alt="Image" id="image1" /></li> 	
            <li><img src= "<%: ViewRes.Views.Home.Index.FlexibilityImage %>" alt="Image" id="image2" /></li> 
            <li><img src= "<%: ViewRes.Views.Home.Index.EstImage %>" alt="Image" id="image3" /></li> 
	</ul>--%>
    
   
    <img src="/Content/Images/Slider_Home/main-img.jpg" alt="" title="<%: ViewRes.Views.Home.Index.ImageMsj1 %>" />
    <img src="/Content/Images/Slider_Home/main-img3.jpg" alt="" title="<%: ViewRes.Views.Home.Index.ImageMsj3 %>" />
    <img src="/Content/Images/Slider_Home/main-img4.jpg" alt="" title="<%: ViewRes.Views.Home.Index.ImageMsj4 %>" />
    <img src="/Content/Images/Slider_Home/encuesta-satisfaccion.jpg" alt="" title="<%: ViewRes.Views.Home.Index.ImageMsj5 %>" />
    <img src="/Content/Images/Slider_Home/evaluacion-empleados.jpg" alt="" title="<%: ViewRes.Views.Home.Index.ImageMsj6 %>" />
  </div> 
  
    <script type="text/javascript" src="/Scripts/jquery.nivo.slider.js"></script>
    <script type="text/javascript">
        $(window).load(function () {
            $('#slider').nivoSlider();
        });
    </script>