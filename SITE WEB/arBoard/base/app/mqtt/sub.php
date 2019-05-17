<?php
 
define('BROKER', 'localhost');
define('PORT', 1883);
define('CLIENT_ID', "pubclient_" + getmypid());

$clientsub = new Mosquitto\Client(CLIENT_ID);
$clientsub->onConnect('connect');
$clientsub->onDisconnect('disconnect');
$clientsub->onSubscribe('subscribe');
$clientsub->onMessage('message');
$clientsub->connect("localhost", 1883, 60);
$clientsub->subscribe('play/general', 1);
 
/*$i = 0; 
while ($i<20) {
        $clientsub->loop();
        $i = $i + 1;
        sleep(1);
}*/
$clientsub->loopForever();

$clientsub->disconnect();
unset($clientsub);
 
function connect($r) {
        echo "I got code {$r}<br/>";
}
 
function subscribe() {
        echo "Subscribed to a topic<br/>";
}
 
function message($message) {
        printf("<br/>%s", $message->payload);
		if($message->payload == "over"){
			$clientsub->exitLoop();
		}
}
 
function disconnect() {
        echo "Disconnected cleanly\n";
}
