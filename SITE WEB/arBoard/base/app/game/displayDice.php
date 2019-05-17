<script>            
            let playerTurnPseudo = "<?php echo $playerTurnPseudo; ?>";
            let diceButtonId = document.getElementById("dice-button");
            if(pseudo == playerTurnPseudo){
                diceButtonId.className = "display-inline";
            } else {
                diceButtonId.className = "display-none";
            }
</script>