<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8" />
        <title>Ar Boarding Game</title>
        <link rel="stylesheet" type="text/css" href="../../css/auth.css">
        <script src="../../include/storage.js"></script>
    </head>
    <body>
        <script>
        if(getLocalStorage() !== null){
            window.location.href="../../prelobby.php";
        }
        </script>

        <div id="formulaire_auth">
            <h1>Authentification</h1>
            <form action="" method="post">
                <label for="email">Entrez votre email : </label>
                <input type="text" name="email" id="email"/> <br> <br>
                <label for="mdp">Entrez votre mot de passe : </label>
                <input type="password" name="mdp" id="mdp"/> <br> <br>
                <input type="submit"/>
            </form>
        </div>

    <?php
        if(isset($_POST['mdp']) && isset($_POST['email'])){
            $email = $_POST['email'];
            $mdp = $_POST['mdp'];
            $cryptmdp = hash("sha256", $mdp);
            include('../../include/connexionBDD.php');
    
            $selectUsers = $bddArBoard->query('SELECT * FROM users');
            while ($donnees = $selectUsers->fetch()) {
                if($email == $donnees['email'] AND $cryptmdp == $donnees['mdp']){
                    $pseudo = $donnees['pseudo'];
                }
            }

            if(isset($pseudo)){
                ?>
                <script>let jspseudo = "<?php echo $pseudo ?>";</script>
                <?php
                echo '<script>setLocalStorage(jspseudo);</script>';
            } else {
                echo("L'adresse mail et le mot de passe que vous avez saisi ne sont pas bons.");
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
