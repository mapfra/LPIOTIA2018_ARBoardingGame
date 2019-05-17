<?php
include('../../include/connexionBDD.php');
if(!empty($_POST['idlob']) AND !empty($_POST['pseudo']) AND !empty($_POST['message'])){
    
    $idlob = $_POST['idlob'];
    $pseudo = $_POST['pseudo'];
    $message = $_POST['message'];
        
    $reqSqlInsertChat = "INSERT INTO chat (IDhexGame, pseudo, message) VALUES ('" . $idlob . "', '" . $pseudo . "', '" . $message . "')";
    if (($bddArBoard->query($reqSqlInsertChat)) == TRUE) {
        echo "<br>Ajout effectué !";
    } else {
        echo "Ajout non effectué";
    }
        
//        $insertion = $bddArBoard->prepare('INSERT INTO chat VALUES(:idlob, :pseudo, :message)');
//        $insertion->execute(array(
//            'IDhexGame' => $idlob,
//            'pseudo' => $pseudo,
//            'message' => $message
//        ));

} else {
    echo "Vous avez oublié de remplir un des champs !";
}
?>

