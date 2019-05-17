<?php

define('BROKER', 'localhost');
define('PORT', 1883);
define('CLIENT_ID', "pubclient_" + getmypid());

$client = new Mosquitto\Client(CLIENT_ID);
$client->connect(BROKER, PORT, 60);
  
$message = "\nJoueur " . $_GET['tokenj'] . "\nValeur " . $_GET['dicevalue'];
$client->publish('play/general', $message, 0, false);
