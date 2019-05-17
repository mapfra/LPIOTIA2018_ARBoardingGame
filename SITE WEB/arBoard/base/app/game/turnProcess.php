<?php
include('../../include/connexionBDD.php');
if(!empty($_POST['newboardvalue']) && !empty($_POST['playerturn'])){
    $newBoardValue = $_POST['newboardvalue'];
    $playerTurn = $_POST['playerturn'];
    $idgame = $_POST['idgame'];
    $reqSqlUpdateGames = "UPDATE games SET gamePic = '" . $newBoardValue . "', playerTurn = " . $playerTurn . " WHERE IDhex LIKE '" . $idgame . "'";
    // $reqSqlInsertChat = "INSERT INTO chat (IDhexGame, pseudo, message) VALUES ('" . $idlob . "', '" . $pseudo . "', '" . $message . "')";
    // echo $reqSqlUpdateGames;
    try {
        if (($bddArBoard->query($reqSqlUpdateGames)) == TRUE) {
                echo "<br>Ajout effectué !";
            } else {
                echo "Ajout non effectué";
            }
    } catch (Exception $e) {
        echo $e;
    }
    // if (($bddArBoard->query($reqSqlUpdateGames)) == TRUE) {
    //     echo "<br>Ajout effectué !";
    // } else {
    //     echo "Ajout non effectué";
    // }
} else {
    echo "Pas les bonnes infos.";
}
?>

