<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8" />
        <title>Ar Boarding Game</title>
        <link rel="stylesheet" type="text/css" href="../../css/insc.css">
        <script src="../../include/storage.js"></script>
    </head>
    <body>
        <script>
        if(getLocalStorage() !== null){
            window.location.href="../../prelobby.php";
        }
        </script>

        <div id="formulaire_insc">
            <h1>Inscription</h1>
            <form action="" method="post">
                <label for="email">Entrez votre email : </label>
                <input type="text" name="email" id="email" required /> <br> <br>
                <label for="email">Entrez votre pseudo : </label>
                <input type="text" name="pseudo" id="pseudo" required /> <br> <br>
                <label for="mdp">Entrez votre mot de passe : </label>
                <input type="password" name="mdp" id="mdp" required /> <br> <br>
                <label for="mdpConf">Entrez à nouveau votre mot de passe : </label>
                <input type="password" name="mdpConf" id="mdpConf" required /> <br> <br>
                <input type="submit"/>
            </form>
        </div>

    <?php
        if(isset($_POST['email'])){
            $email = $_POST['email'];
            $pseudo = $_POST['pseudo'];
            $mdp = $_POST['mdp'];
            $mdpConf = $_POST['mdpConf'];
            $ok = 1;
            
            if($mdp !== $mdpConf) {
                echo "<br>Les deux mots de passe ne correspondent pas. Veuillez recommencer.";
            } else {
                include('../../include/connexionBDD.php');
    
                $selectUsers = $bddArBoard->query('SELECT * FROM users');
                while ($donnees = $selectUsers->fetch()) {
                        if($email == $donnees['email']) {
                            echo "<br>Cette adresse mail a déjà été utilisée. Veuillez recommencer.";
                            $ok = 0;
                        }
                        if($pseudo == $donnees['pseudo']){
                            echo "<br>Ce pseudo a déjà été utilisé. Veuillez recommencer.";
                            $ok = 0;
                        }
                    
                }
                
                if($ok) {
                    $cryptmdp = hash("sha256", $mdp);
                        $reqSqlInsert = "INSERT INTO users (email, pseudo, mdp) VALUES ('" . $email . "', '" . $pseudo . "', '" . $cryptmdp . "')";
                        
                        if (($bddArBoard->query($reqSqlInsert)) == TRUE) {
//                            echo "<br>Inscription effectuée !"; TODO TOAST
                            ?>
                            <script>let jspseudo = "<?php echo $pseudo ?>";</script>
                            <?php
                            echo '<script>setLocalStorage(jspseudo);</script>';
                        } else {
                            echo "<br>Il y a eu une erreur, veuillez réessayer.";
                        }
                }
            }
        }
    ?>

    
    <script>
        if(getLocalStorage() !== null){
            window.location.href="../../prelobby.php";
        }
        </script>
    </body>
</html>
