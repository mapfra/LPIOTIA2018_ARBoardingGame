<script>
let timer = 15;
            let timerDiv = document.getElementById("timer");
            function loadPage(){
                setTimeout( function(){
                    timerDiv.innerHTML = timer;
                    if(timer == 0){
                        let idgame = "<?php echo $idparty ?>";
                        window.location.href="game.php?idgame="+idgame+"";
                    }
                    timer--;
                    loadPage();
                }, 1000); // Exécution toutes les secondes
            }
            loadPage();
</script>
