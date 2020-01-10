// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.6.2

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace IntroBotFramework.Bots
{
    public class EchoBot : ActivityHandler
    {
        private BotState _userState;
        public EchoBot( UserState userState)
        {
            _userState = userState;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext,
            CancellationToken cancellationToken)
        {

            
            var userStateAccessors = _userState.CreateProperty<UserProfile>(nameof(UserProfile));
            var userProfile = await userStateAccessors.GetAsync(turnContext, () => new UserProfile());

            if (string.IsNullOrEmpty(userProfile.Nama))
            {
                if (!userProfile.PromptedUserForName)
                {
                    await turnContext.SendActivityAsync("Masukkan nama :");
                    userProfile.PromptedUserForName = true;
                }
                else
                {
                    userProfile.Nama = turnContext.Activity.Text?.Trim();
                    await turnContext.SendActivityAsync($"Hi {userProfile.Nama}, Silahkan Masukkan Alamat anda:");
                }
               
            }
            else 
            {
                userProfile.Alamat = turnContext.Activity.Text?.Trim();
                var textMsg =
                    $"Data anda sebagai berikut telah kami simpan. Nama {userProfile.Nama} dan Alamat {userProfile.Alamat}. Untuk memulai pendaftaran ketik 'Hi'";
                userProfile.Nama = string.Empty;
                userProfile.Alamat = string.Empty;
                userProfile.PromptedUserForName = false;
                await turnContext.SendActivityAsync(textMsg);
            }
           
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.OnTurnAsync(turnContext, cancellationToken);

            await _userState.SaveChangesAsync(turnContext, false, cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(CreateActivityWithTextAndSpeak($"Selamat datang di latihan Chatbot.."), cancellationToken);
                }
            }
        }

        private IActivity CreateActivityWithTextAndSpeak(string message)
        {
            var activity = MessageFactory.Text(message);
            string speak = @"<speak version='1.0' xmlns='https://www.w3.org/2001/10/synthesis' xml:lang='en-US'>
              <voice name='Microsoft Server Speech Text to Speech Voice (en-US, JessaRUS)'>" +
              $"{message}" + "</voice></speak>";
            activity.Speak = speak;
            return activity;
        }
    }
}
