<?php
    include('./include/connexionBDD.php');
?>
<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8" />
        <link rel="stylesheet" type="text/css" href="./css/lobby.css">
        <link rel="stylesheet" type="text/css" href="./css/generic.css">
        <script src="https://code.jquery.com/jquery.js"></script>
        <script src="./include/storage.js"></script>
        <title>Ar Boarding Game</title>
        <script>
            function requestRemote(){
                window.location.href="lobby.php?idgame=<?php echo $_GET['idgame'] ?>&pseudoPlayer=<?php echo $_GET['pseudoPlayer'] ?>&receivemode=true";
            }
        </script>
    </head>

    <body>
        <script>
            let pseudo = getLocalStorage();
            if(pseudo == null){ // Vérifie si le pseudo n'est pas en Stockage Local (ce qui veut dire que l'utilisateur n'est pas connecté)
                window.location.href="../index.php";
            }
        </script>

        <?php include("./app/lobby/processLobby.php"); ?> <!-- Processus de récupérations des éléments de la BD et des nouveau joueurs -->

        <div id="main_wrapper" class="flex-container-column">
            <?php include('./include/header.php'); ?>
            <div id="content" class="flex-container-row">
                    <div id="players"> <!-- Colonne des joueurs -->
                        <h2>Players</h2>
                        <p>
                            <span id="p1"><?php if(isset($player1)) {echo $player1;} else {echo 'Waiting player 1';} ?></span>
                        </p>
                        <p>
                            <span id="p2"><?php if(isset($player2)) {echo $player2;} else {echo 'Waiting player 2';} ?></span>
                        </p>
                        <p>
                            <span id="p3"><?php if(isset($player3)) {echo $player3;} else {echo 'Waiting player 3';} ?></span>
                        </p>
                        <p>
                            <span id="p4"><?php if(isset($player4)) {echo $player4;} else {echo 'Waiting player 4';} ?></span>
                        </p>
                    </div>
                    <div id="midColumn"> <!-- Colonne du start et de la requête d'appairage -->
                        <div id="startGame">
                            <?php
                            $launchegamelink = "./game.php?idgame=" . $idlob . "";
                            ?>
                            <h1><a href="<?php echo $launchegamelink ?>">Lancer la partie !</a></h1>
                        </div>
                        <div id="requestRemoteMqtt">
                                <br>
                                <br>
                                <button id="requestButton" onclick="requestRemote()">Requête d'appairage MQTT</button>
                                <?php
                                    if(isset($_GET['receivemode'])){
                                        include('./mqtt/sub.php');
                                    }
                                ?>
                        </div>
                    </div>
                    <div id="chat"> <!-- Colonne de l'espace de discussion -->
                        <h2>Espace de discussion</h2>
                        <div id="messages">
                            <?php
                            $requete = $bddArBoard->query('SELECT * FROM chat WHERE IDhexGame LIKE "' . $idlob . '" ORDER BY ID DESC LIMIT 0,10'); // Récupération des messages
                            while($donnees = $requete->fetch()){
                                echo "<p id=\"" . $donnees['ID'] . "\">" . $donnees['pseudo'] . " : " . $donnees['message'] . "</p>";
                            }
                            $requete->closeCursor();
                            ?>
                        </div>
                        <br>
                        <form action="./app/chat/processChat.php" method="POST"> <!-- Formulaire d'envoie d'un message -->
                            <input type="text" name="idlob" id="idlob" class="display-none" value="<?php echo $idlob ?>">
                            <input type="text" name="pseudo" id="pseudo" class="display-none" value="<?php echo $pseudoPlayer ?>">
                            <input type="text" name="message" id="message">
                            <input type="submit" name="submit" id="submitM" value="Envoyer">
                        </form>
                    </div>
            </div>
        </div>
        
        <script src="./app/lobby/scripts/sendMessage.js"></script> <!-- Fonction jQuery pour envoyer le message en AJAX -->
        <script src="./app/lobby/scripts/loadMessage.js"></script> <!-- Fonction pour charger un message -->
        
    </body>
</html>
