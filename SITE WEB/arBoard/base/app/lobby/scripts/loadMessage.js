        function charger(){
            setTimeout( function(){
                let firstID = $('#messages p:first').attr('ID');
                let idlob = $('#idlob').val();
                $.ajax({
                    url : "./app/chat/loadChat.php?ID=" + firstID + "&idgame=" + idlob,
                    type : "GET",
                    success : function(html){
                        $('#messages').prepend(html);
                    }
                });
                charger();
            }, 1000); // Exécution toutes les secondes
        }
        charger();