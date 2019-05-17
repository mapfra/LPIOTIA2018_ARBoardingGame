<script>
            $('#submitTurn').click(function(e){
                e.preventDefault(); // Empêcher le bouton d'envoyer le formulaire

                let newboardvalue = $('#newboardvalue').val();
                let playerturn = $('#playerturn').val();
                let idgame = $('#idgame').val();
                if(newboardvalue != "" && playerturn != "" && idgame != ""){
                    // console.log(newboardvalue);
                    // console.log(playerturn);
                    // alert("newBoardValue=" + newboardvalue + "&playerTurn=" + playerturn);
                    $.ajax({
                        url : "./app/game/turnProcess.php",
                        type : "POST",
                        data : "newboardvalue=" + newboardvalue + "&playerturn=" + playerturn + "&idgame=" + idgame,
                        dataType : 'html'
                    });
                }
            });
</script>