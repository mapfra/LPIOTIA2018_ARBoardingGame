<?php
    include('./include/connexionBDD.php');
?>
<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8" />
        <link rel="stylesheet" type="text/css" href="./css/game.css">
        <link rel="stylesheet" type="text/css" href="./css/generic.css">
        <script src="https://code.jquery.com/jquery.js"></script>
        <script src="./app/game/scripts/turn.js"></script>
        <script type="text/javascript" src="./app/game/scripts/Horse.js"></script>
        <script type="text/javascript" src="./app/game/scripts/Board.js"></script>
        <script src="./include/storage.js"></script>
        <title>Ar Boarding Game</title>
    </head>

    <body>
        <script>
            let pseudo = getLocalStorage();
            if(pseudo == null){ // Vérifie si le pseudo n'est pas en Stockage Local (ce qui veut dire que l'utilisateur n'est pas connecté)
                window.location.href="../index.php";
            }
        </script>

        <?php include("./app/game/processGame.php") ?>
        
        <input type="text" name="boardValue" id="boardValue" class="hidden" value="<?php echo htmlspecialchars($board) ?>"> <!-- Récupération du plateau de jeu côté client -->
        <div id="main_wrapper" class="flex-container-column">
            <?php include('./include/header.php'); ?>
            <div id="content" class="flex-container-row">
                <div id="left-side" class="game-flex-container-column">
                    <div id="players"> <!-- Colonne des joueurs -->
                        <h2>Players</h2>
                        <p>
                            <span id="p1" class="<?php if($playerTurn == 1) {echo "player-turn";} else {echo "not-player-turn";} ?>"><?php if(isset($player1)) {echo $player1;} else {echo 'Waiting player 1';} ?></span>
                        </p>
                        <p>
                            <span id="p2" class="<?php if($playerTurn == 2) {echo "player-turn";} else {echo "not-player-turn";} ?>"><?php if(isset($player2)) {echo $player2;} else {echo 'Waiting player 2';} ?></span>
                        </p>
                        <p>
                            <span id="p3" class="<?php if($playerTurn == 3) {echo "player-turn";} else {echo "not-player-turn";} ?>"><?php if(isset($player3)) {echo $player3;} else {echo 'Waiting player 3';} ?></span>
                        </p>
                        <p>
                            <span id="p4" class="<?php if($playerTurn == 4) {echo "player-turn";} else {echo "not-player-turn";} ?>"><?php if(isset($player4)) {echo $player4;} else {echo 'Waiting player 4';} ?></span>
                        </p>
                    </div>
                </div>
                <div id="pageContent"> <!-- Colonne du plateau -->
                    <h2>Partie en cours</h2>
                    <h2 id="playerText"></h2>
                    <div id="plateau">
                        <?php 
                            echo $board;
                        ?>
                    </div>
                </div>
                <div id="right-side" class="game-flex-container-column"> <!-- Colonne du dé et du timer -->
                    <div id="form-conf-turn" class="display-none">
                        <form action="./app/game/turnProcess.php" method="POST">
                            <input type="text" name="newboardvalue" id="newboardvalue" class="display-none">
                            <input type="text" name="playerturn" id="playerturn" class="display-none" value="<?php echo $playerTurn ?>">
                            <input type="text" name="idgame" id="idgame" class="display-none" value="<?php echo $idparty ?>">
                            <input type="submit" name="submit" id="submitTurn" value="Confirmer tour" onclick="playTurn()">
                        </form>
                    </div>
                    <div id="dice-wrapper">
                        <div>
                            <input id="horse1" type="radio" name="horse" value="1" checked>
                            <label for="horse1">Cheval 1</label>
                        </div>
                        <div>
                            <input id="horse2" type="radio" name="horse" value="1">
                            <label for="horse2">Cheval 2</label>
                        </div>

                        <button id="dice-button" onclick="play()">Lancer dé</button>
                        <div id="de-row">
                            <div id="de-zone">
                                Résultat : 
                                <span id="result-de">-</span>
                            </div>
                        </div>
                    </div>
                    <div id="timer-wrapper">
                        <h2>Timer</h2>
                        <div id="timer">
                            15
                        </div>
                    </div>
                    <div id="console_logs">     
                    <h2>Logs</h2>       
                    <?php 
                        if(isset($_GET['tokenj']) && isset($_GET['dicevalue'])){ // Quand j'ai des données de jeu à envoyer par MQTT
                            include('./app/mqtt/pub.php');
                        }
                    ?>
                    <?php
                        if(isset($_GET['receivemode'])){ // Quand je veux recevoir des données via MQTT et les afficher dans les logs
                            include('./app/mqtt/sub.php');
                        }
                    ?>
                    </div>
                </div>
            </div>
        </div>
        
        <?php include("./app/game/displayDice.php") ?> <!-- Permet de ne pas faire apparaître le dé quand ce n'est pas à soi de jouer -->
        <?php include("./app/game/sendConfirmation.php") ?> <!-- Permet d'envoyer la confirmation de jeu du tour actuel -->
        <?php include("./app/game/timer.php") ?> <!-- Permet d'avoir un timer de jeu -->
        <script> 
            let _Board = new Board();

            _Board.addHorse("Horse1", "green");
            _Board.addHorse("Horse2", "blue");
            _Board.addHorse("Horse3", "red");
            _Board.addHorse("Horse4", "orange");
            _Board.addHorse("Horse5", "green");
            _Board.addHorse("Horse6", "blue");
            _Board.addHorse("Horse7", "red");
            _Board.addHorse("Horse8", "orange");

            function play(){
                lancerDe(<?php echo $playerTurn ?>);                
            }
        </script> <!-- Permet d'initialiser le tour de jeu -->
    </body>
</html>
