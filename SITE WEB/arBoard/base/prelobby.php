<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8" />
        <title>Ar Boarding Game</title>
        <link rel="stylesheet" type="text/css" href="./css/generic.css">
        <link rel="stylesheet" type="text/css" href="./css/prelobby.css">
        <script src="./include/storage.js"></script>
        <script>
            function onPageLoad() {
                let pseudo = getLocalStorage();
                document.getElementById("pseudoInput").value = pseudo;
            } 
        </script>
    </head>
    <body onLoad="onPageLoad()"> <!-- Page servant à créer un lobby -->
        <div id="wait_lobby">
<!--            <button onclick="joinLobby()">Rejoindre une partie</button> onclick="createLobby()"-->
            <form method="post" action="./app/lobby/createLobby.php">
                <input type="text" name="pseudoInput" id="pseudoInput" class="display-none" />
                <input class="input-sub" type="submit" value="Créer une partie"/>
            </form>
        </div>
    </body>
</html>
