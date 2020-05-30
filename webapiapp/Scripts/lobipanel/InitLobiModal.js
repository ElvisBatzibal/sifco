$(document).keyup(function (event) {
    if (event.which == 27) {
        $('.lobipanel').lobiPanel('close');
    }
});

$(function () {
    $('body').on('click', '.close-lobipanel', function (e) {
        e.preventDefault();
        $('.lobipanel').lobiPanel('close');
    });
});


var _idlobipanel = function (n) {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (var i = 0; i < n; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;
};


var _generarLobipanel = function (idlobi, lobiTitle) {
    return $('<div id="' + idlobi + '" class="panel panel-default lobiPanel"></div>')
               .append('<div class="panel-heading"><div class="panel-title">' + lobiTitle + ' </div></div>')
               .append('<div class="panel-body"></div>');
};

var ValidarisEmpty = function (data) {
    if (typeof (data) === 'object') {
        if (JSON.stringify(data) === '{}' || JSON.stringify(data) === '[]') {
            return true;
        } else if (!data) {
            return true;
        }
        return false;
    } else if (typeof (data) === 'string') {
        if (!data.trim()) {
            return true;
        }
        return false;
    } else if (typeof (data) === 'undefined') {
        return true;
    } else {
        return false;
    }
}

function ModalLobiPanelDirect(option)
{
    var objects = ['baseurl', 'bodyheight', 'bodywidth', 'lobiTitle'];//Optimizarlo a objetos
    var BaseUrl = "" + option.baseurl;
    var bodywidth=""+option.bodyheight;
    var bodyheight = "" + option.bodywidth;
    var lobiTitle = "" + option.lobiTitle;
    CrearModalLobiPanel(BaseUrl, bodywidth, bodyheight, lobiTitle);
}

function CrearModalLobiPanel(url, bodywidth, bodyheight, lobiTitle) {
    bodywidth = (bodywidth == null || ValidarisEmpty(bodywidth)==true) ? 400 : bodywidth;
    bodyheight = (bodyheight == null || ValidarisEmpty(bodyheight) == true) ? 400 : bodyheight;
    lobiTitle = (lobiTitle == null || ValidarisEmpty(lobiTitle) == true) ? "" : lobiTitle;

    var idlobi = _idlobipanel(10);
    $("#banerlobipaneldefault").append(_generarLobipanel(idlobi, lobiTitle));
    var _idlobi = "#" + idlobi;
     $(_idlobi).lobiPanel({
        unpin: false,
        editTitle: false,
        expand:false,
        autoload: true,        
    });
    var instance = $(_idlobi).data('lobiPanel');
    //call the methods
    instance.unpin();
    //method chaining is also available
    instance.setLoadUrl(url)
    .load();  
    //Definicion de tamaño
    var screenwidth = window.screen.width;
    var screenheight = window.screen.height;
    if (screenwidth > 1024)
    {        
       var bodywidth = ((bodywidth.includes("%") == true)? (screenwidth*parseInt(bodywidth)/100) : bodywidth);        
        var posX = (screenwidth - bodywidth) / 2;        
        $('.lobipanel').lobiPanel("setPosition", posX, 50);
        //$(_idlobi).lobiPanel({
        //    bodyHeight: 600,
        //});

        //Tamaño setHeight
        $(_idlobi).lobiPanel("setHeight", bodyheight);
        //Tamaño setWidth
        $(_idlobi).lobiPanel("setWidth", bodywidth);
        //Posicion

    } else {
        $(_idlobi).lobiPanel("setSize", screenwidth, screenheight);
        $('.lobipanel').lobiPanel("setPosition", 0, 50);
    }    
}
$(function () {
    $('body').on('click', '.Show-modallobipanel', function (e) {       
        e.preventDefault();
        var BaseUrl = $(this).attr("href");
        var bodywidth = $(this).attr("data-width");
        var bodyheight = $(this).attr("data-height");
        var lobiTitle = $(this).attr("data-title");            
        CrearModalLobiPanel(BaseUrl, bodywidth, bodyheight, lobiTitle);
    });  
});
