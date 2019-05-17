$('#submitM').click(function(e){
    e.preventDefault(); // Empêcher le bouton d'envoyer le formulaire

    let idlob = $('#idlob').val();
    let pseudo = $('#pseudo').val();
    let message = $('#message').val();
    if(pseudo != "" && message != ""){
        $.ajax({
            url : "./app/chat/processChat.php",
            type : "POST",
            data : "idlob=" + idlob + "&pseudo=" + pseudo + "&message=" + message,
            dataType : 'html'
        });
        
//                    $('#messages').append("<p>" + pseudo + " : " + message + "</p>");
    }
});