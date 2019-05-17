using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Common.HorseGame.States
{
    class ConnectToGame: IState
    {
        public void Execute()
        {
            GameContext.MQTT.SetBrokerAddress("10.0.4.9");
            GameContext.MQTT.SetBrokerPort("1883");

            GameContext.DisplayLabel.text = "Trying to connect";

            GameContext.Subscribe("Connected", Connected);
            GameContext.MQTT.Connect();
        }

        public void Connected(System.Object parameter = null)
        {
            GameContext.UnSubscribe("Connected");
            GameContext.MQTT.SubscribeTopic("play/general");
            GameContext.MQTT.Publish("over", "play/general");

            GameContext.DisplayLabel.text = "Connected";

            GameContext.ActualState = new WaitForAGame();

        }
    }
}
