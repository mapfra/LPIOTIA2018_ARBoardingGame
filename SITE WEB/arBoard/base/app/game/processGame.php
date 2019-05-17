<?php 
        $idparty = $_GET['idgame'];
        $selectGame = $bddArBoard->query("SELECT * FROM games WHERE IDhex LIKE '" . $idparty . "'");
        foreach($selectGame as $value) {
            $board = $value[1];
            $player1 = $value[2];
            $player2 = $value[3];
            $player3 = $value[4];
            $player4 = $value[5];
            $playerTurn = $value[6];
        }
        if($playerTurn==NULL){
            $playerTurn = 1;
            $playerTurnPseudo = $player1;
        } else if($playerTurn==1) {
            $playerTurn = 2;
            $playerTurnPseudo = $player2;
        } else if($playerTurn==2) {
            $playerTurn = 3;
            $playerTurnPseudo = $player3;
        } else if($playerTurn==3) {
            $playerTurn = 4;
            $playerTurnPseudo = $player4;
        } else if($playerTurn==4) {
            $playerTurn = 1;
            $playerTurnPseudo = $player1;
        }
?>