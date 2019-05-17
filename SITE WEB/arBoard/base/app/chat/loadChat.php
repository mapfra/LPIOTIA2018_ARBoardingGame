<?php
include('../../include/connexionBDD.php');

if(($_GET['ID']) != "undefined"){

    $ID = (int) $_GET['ID'];
    $idlob = (int) $_GET['idgame'];
    $requete = $bddArBoard->prepare('SELECT * FROM chat WHERE ID > :ID ORDER BY ID DESC');
    $requete->execute(array("ID" => $ID));

    $messages = null;

    while($donnees = $requete->fetch()){
        $messages .= "<p id=\"" . $donnees['ID'] . "\">" . $donnees['pseudo'] . " : " . $donnees['message'] . "</p>";
    }

    echo $messages;

}

?>

