//using D_One.Core.DofusBehavior;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Core.Models
//{
//    public class FightResult
//    {
//        public int Duration { get; private set; }
//        public bool Win { get; private set; } // looser  = 0 winner = 2
//        public int Xp { get; private set; }
//        public string[] Items { get; private set; }
//        public string Kamas { get; private set; }

//        //GE3665|1  |0|2;1   ;sowen   ;200;0;7407232000;7407232000;9223372036854775807; ; ; ;; |0;2  ;Hymeduz     ;200;0;7407232000;7407232000;9223372036854775807; ; ; ;
//        public FightResult Parse (string packet, GameManager gm)
//        {
//            packet = packet.Substring(2);
//            string[] _loc4 = packet.Split('|');
//            int _loc5 = -1;
//            if (int.TryParse(_loc4[0], out int duration))
//                Duration = duration;
//            else
//            {
//                string[] _loc6 = _loc4[0].Split(';');
//                Duration = int.Parse(_loc6[0]);
//                _loc5 = int.Parse(_loc6[1]);
//            }

//            int _loc7 = int.Parse(_loc4[1]);
//            int _loc8 = int.Parse(_loc4[2]);
//            if (_loc8 == 0) return null;
//        }

//        private bool ParsePlayerData(int senderId, string[] data, int kamas, GameManager gm)
//        {
//            int startIndex = 3;
//            string[] _loc10 = data[startIndex].Split(';');
//            if (int.Parse(_loc10[0]) != 6)
//            {
//                int playerId = int.Parse(_loc10[1]);
//                if (playerId != gm.CharacterPlayerManager.Id) return false;

//                if (int.Parse(_loc10[0]) == 0) Win = false;
//                else Win = true;

//                Xp = int.Parse(_loc10[6]);
//                Kamas = _loc10[12];
//            }
//            else
//            {
//                string[] _loc12 = _loc10[1].Split(',');
//                Kamas = _loc10[2];
//                kamas = kamas + int.Parse(Kamas);
//            } // end else if
//            Items = new int[];
//            var _loc14 = _loc12.length;
//            while (--_loc14 >= 0)
//            {
//                var _loc15 = _loc12[_loc14].split("~");
//                var _loc16 = Number(_loc15[0]);
//                var _loc17 = Number(_loc15[1]);
//                if (_global.isNaN(_loc16))
//                {
//                    break;
//                } // end if
//                if (_loc16 == 0)
//                {
//                    continue;
//                } // end if
//                var _loc18 = new dofus.datacenter.Item(0, _loc16, _loc17);
//                _loc11.items.push(_loc18);
//            } // end while
//            switch (Number(_loc10[0]))
//            {
//                case 0:
//                    {
//                        oResults.loosers.push(_loc11);
//                        break;
//                    }
//                case 2:
//                    {
//                        oResults.winners.push(_loc11);
//                        break;
//                    }
//                case 5:
//                    {
//                        oResults.collectors.push(_loc11);
//                        break;
//                    }
//                case 6:
//                    {
//                        eaFightDrop = eaFightDrop.concat(_loc11.items);
//                    }
//            } // End of switch
//            ++_loc9;
//            if (_loc9 < aTmp.length)
//            {
//                this.addToQueue({ object: this, method: this.parsePlayerData, params: [oResults, _loc9, nSenderID, aTmp, nFightType, nKamaDrop, eaFightDrop]
//    });
//        }
//        else
//        {
//            this.onParseItemEnd(nSenderID, oResults, eaFightDrop, nKamaDrop);
//        } // end else if
//    };
//    _loc1.onParseItemEnd = function(nSenderID, oResults, eaFightDrop, nKamaDrop)
//{
//    if (eaFightDrop.length)
//    {
//        var _loc6 = Math.ceil(eaFightDrop.length / oResults.winners.length);
//        var _loc7 = 0;

//        while (++_loc7, _loc7 < oResults.winners.length)
//            {
//            var _loc8 = eaFightDrop.length;
//            oResults.winners[_loc7].kama = Math.ceil(nKamaDrop / _loc6);
//            if (_loc7 == oResults.winners.length - 1)
//            {
//                _loc6 = _loc8;
//            } // end if
//            var _loc9 = _loc8 - _loc6;

//            while (++_loc9, _loc9 < _loc8)
//                {
//                oResults.winners[_loc7].items.push(eaFightDrop.pop());
//            } // end while
//        } // end while
//    } // end if
//    if (nSenderID == this.api.datacenter.Player.ID)
//    {
//        this.aks.GameActions.onActionsFinish(String(nSenderID));
//    } // end if
//    this.api.datacenter.Game.isRunning = false;
//    var _loc10 = this.api.datacenter.Sprites.getItemAt(nSenderID).sequencer;
//    this._bIsBusy = false;
//    if (_loc10 != undefined)
//    {
//        _loc10.addAction(false, this.api.kernel.GameManager, this.api.kernel.GameManager.terminateFight);
//        _loc10.execute(false);
//    }
//    else
//    {
//        ank.utils.Logger.err("[AKS.Game.onEnd] Impossible de trouver le sequencer");
//        ank.utils.Timer.setTimer(this, "game", this.api.kernel.GameManager, this.api.kernel.GameManager.terminateFight, 6000);
//    } // end else if
//    this.api.kernel.TipsManager.showNewTip(dofus.managers.TipsManager.TIP_FIGHT_ENDFIGHT);
//}
//    }
//}
