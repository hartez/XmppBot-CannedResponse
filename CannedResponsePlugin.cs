using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using Newtonsoft.Json;
using SimpleConfig;
using XmppBot.Common;

namespace XmppBot_CannedResponse
{
    [Export(typeof(IXmppBotPlugin))]
    public class CannedResponsePlugin : IXmppBotPlugin
    {
        private IList<PotentialResponse> _responses;

        public CannedResponsePlugin()
        {
            var config = Configuration.Load<CannedResponseConfig>();
            Initialize(config);
        }

        public CannedResponsePlugin(string configPath)
        {
            var config = Configuration.Load<CannedResponseConfig>(configPath: configPath);
            Initialize(config);
        }

        public string Evaluate(ParsedLine line)
        {
            if(line.IsCommand)
            {
                return null;
            }

            foreach(PotentialResponse potentialResponse in _responses)
            {
                if(potentialResponse.ExactMatch)
                {
                    if(line.Raw == potentialResponse.Trigger)
                    {
                        return potentialResponse.Text;
                    }
                }

                if(potentialResponse.ExactMatch == false)
                {
                    if(line.Raw.ToLower().Contains(potentialResponse.Trigger.ToLower()))
                    {
                        return potentialResponse.Text;
                    }
                }
            }

            return null;
        }

        public string Name
        {
            get { return "Canned Response"; }
        }

        private void Initialize(CannedResponseConfig config)
        {
            if(!String.IsNullOrEmpty(config.ResponseFilePath))
            {
                ReadResponses(config.ResponseFilePath);
            }
            else
            {
                _responses = new List<PotentialResponse>();
            }
        }

        private void ReadResponses(string responseFilePath)
        {
            string responseData = File.ReadAllText(responseFilePath);

            var potentialResponses = JsonConvert.DeserializeObject<List<PotentialResponse>>(responseData);

            _responses = potentialResponses;
        }
    }
}