//$("document").ready(test ());
$(document).ready(function()
{
    $("#msg").hide();
    notif();
    /*/
  if(($("#notification_count").val()==='')||($("#notification_count").val()===0)){
      
      $("#notification_count").hide();
  }  
  //*/
$("#notificationLink").click(function()
{
        affiche();
$("#notificationContainer").fadeToggle(300);
//$("#notification_count").fadeOut("slow");
return false;
});

$("#notificationdel").click(function()
{
        del();
//return false;
});


//Document Click
$(document).click(function()
{
//$("#notificationContainer").hide();
});
//Popup Click
/*/
$("#notificationContainer").click(function()
{
return false
});
//*/
    $("#favorisLink").click(function()
{afficheFav();
$("#favorisContainer").fadeToggle(300);
//$("#favoris_count").fadeOut("slow");
return false;
});

$("#favorisdel").click(function()
{
       // del();
});
$("#fermer").click(function()
{
       $("#msg").hide();
});
    $("#messagerieLink").click(function()
{afficheContact();
$("#messagerieContainer").fadeToggle(300);
//$("#messagerie_count").fadeOut("slow");
return false;
});

$("#messagerieFooter").click(function()
{
       $("#msg").fadeToggle();
       listeContact();
});
});
function notif(){
    {
    $.ajax({
       type : 'get',
       url:Routing.generate('nbrNotif', null),
            //url : 'http://localhost/REAWEbGit/webrea/web/app_dev.php/notification/nbrNotif',
        success: function(data) {
           document.getElementById("notification_count").innerHTML=data.nbr;
             if((data.nbr==='0')){
      
      $("#notification_count").hide();
  }
           $("#notification_count").show();
        }
    });
}  
}
function affiche(){
    {
    $.ajax({
       type : 'get',
       url:Routing.generate('notification', null),
            //url : 'http://localhost/REAWEbGit/webrea/web/app_dev.php/notification/',
        success: function(data) {
           document.getElementById("notificationsBody").innerHTML=data.listeNotif;
          // $("#notification_count").show();
        }
    });
}  
}
function del(iddel){
    {
    $.ajax({
       type : 'get',
       url:Routing.generate('notification_delete',{id:iddel}),
        success: function(data) {
           document.getElementById("notificationsBody").innerHTML=data.listeNotif;
          // $("#notification_count").show();
        }
    });
}  
}
function ajouterFav(idfav){
    {
    $.ajax({
       type : 'get',
       url:Routing.generate('favoris_new',{id:idfav}),
        success: function(data) {
                alert(data.msg);
        }
    });
}  
}
function afficheFav(){
    {
    $.ajax({
       type : 'get',
       url:Routing.generate('favoris', null),
        success: function(data) {
           document.getElementById("favorissBody").innerHTML=data.listeFav;
        }
    });
}  
}
function delFav(iddel){
    {
    $.ajax({
       type : 'get',
       url:Routing.generate('favoris_delete',{id:iddel}),
        success: function(data) {
           document.getElementById("favorissBody").innerHTML=data.listeFav;
          // $("#notification_count").show();
        }
    });
}  
}
function afficheContact(){
    {
    $.ajax({
       type : 'get',
       url:Routing.generate('contact', null),
        success: function(data) {
           document.getElementById("messageriesBody").innerHTML=data.listeMessage;
        }
    });
}  
}
function delMessage(iddel){
    {
    $.ajax({
       type : 'get',
       url:Routing.generate('contact_delete',{id:iddel}),
        success: function(data) {
           document.getElementById("messageriesBody").innerHTML=data.listeMessage;
          // $("#notification_count").show();
        }
    });
}  
}
function envoyerMessage(){
    {
        id=$("#dest").val();
        sujet=$("#sujet").val();
        type=$("#type").val();
        contenu=$("#contenu").val();
    $.ajax({
       type : 'get',
       
       url:Routing.generate('contact_new',{id:id,sujet:sujet,type:type,contenu:contenu}),
        success: function(data) {
            alert("message envoyer");
                    $("#sujet").val("");
        $("#type").val("");
        $("#contenu").val("");
           //document.getElementById("messageriesBody").innerHTML=data.listeMessage;
          // $("#notification_count").show();
        }
    });
}  
}
function listeContact(){
    {
    $.ajax({
       type : 'get',
       
       url:Routing.generate('contact_liste'),
            beforeSend: function() {
                $("#dest option").remove();
                
            },
        success: function(data) {
            //*/
            $.each(data.liste,function(index,value){
              $("#dest").append($('<option>',{value:value.id,text:value.nom}));
             // $("#dest").append($('<option>',{value:value.id}));
            });
            //*/
            
           //document.getElementById("messageriesBody").innerHTML=data.listeMessage;
          // $("#notification_count").show();
        }
    });
}  
}