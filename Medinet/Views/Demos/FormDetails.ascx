<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.DemoViewModel>" %>

<div class="span-24 column last"> 
    
    <div class="span-8 column">
        <div class="span-24 last"><h4>Compañía</h4></div>
        <div class="span-24 last"><%: Model.company%></div> 
    </div>

    <div class="span-8 column">
        &nbsp;
    </div>

    <div class="span-8 column last">
        &nbsp;
    </div>

    <div class="clear"></div>
    
    <div class="span-8 column">
        <div class="span-24 last"><h4>Correo de contacto</h4></div>
        <div class="span-24 last"><%: Model.email %></div> 
    </div>

    <div class="span-8 column">
        <div class="span-24 last"><h4>Usuario para ver reportes</h4></div>
        <div class="span-24 last"><%: Model.manager %></div> 
    </div>

    <div class="span-8 column last">
        <div class="span-24 last"><h4>Contraseña</h4></div>
        <div class="span-24 last"><%: Model.password %></div> 
    </div>

    <div class="span-8 column">
        <div class="span-24 last"><h4>Código de evaluación</h4></div>
        <div class="span-24 last"><%: Model.code %></div> 
    </div>

    <div class="span-8 column">
        <div class="span-24 last"><h4>Número de empleados a medir</h4></div>
        <div class="span-24 last"><%: Model.employees %></div> 
    </div>

    <div class="span-8 column last">
        <div class="span-24 last"><h4>Número de evaluaciones realizadas</h4></div>
        <div class="span-24 last"><%: Model.evaluations %></div> 
    </div>

    <div class="span-8 column">
        <div class="span-24 last"><h4>Número de semanas</h4></div>
        <div class="span-24 last"><%: Model.weeks %></div> 
    </div>

    
    <div class="span-8 column">
        <div class="span-24 last"><h4>Desde</h4></div>
        <div class="span-24 last"> <%: Model.startDate%></div>
    </div>

    <div class="span-8 column last">
        <div class="span-24 last"><h4>Hasta</h4></div>
        <div class="span-24 last"> <%: Model.endDate%></div>
    </div>
    
</div>