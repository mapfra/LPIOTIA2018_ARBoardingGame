<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8" />
        <title>Ar Boarding Game</title>
        <link rel="stylesheet" type="text/css" href="../../css/generic.css">
        <script src="../../include/storage.js"></script>
    </head>
    <body>
        <script>
            let pseudo = getLocalStorage();
            <?php
            $idlob = $_GET['idgame'];
            ?>
            window.location.href="../../lobby.php?idgame=<?php echo $idlob ?>&pseudoPlayer=" + pseudo;
        </script>
    </body>
</html>
