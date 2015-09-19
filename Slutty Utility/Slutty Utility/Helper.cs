﻿using LeagueSharp;
using LeagueSharp.Common;

namespace Slutty_Utility
{
    internal class Helper
    {       
       public static Obj_AI_Hero Player { get { return ObjectManager.Player; } }
       public static Menu Config;
       public static void AddBool(Menu menu, string displayName, string name, bool value)
       {
           menu.AddItem(new MenuItem(name, displayName).SetValue(value));
       }

       public static void AddValue(Menu menu, string displayName, string name, int startVal, int minVal = 0, int maxVal = 100)
       {
           menu.AddItem(new MenuItem(name, displayName).SetValue(new Slider(startVal, minVal, maxVal)));
       }

        public static void AddKeyBind(Menu menu, string displayName, string name, char key, KeyBindType type)
        {
            menu.AddItem(new MenuItem(name, displayName).SetValue(new KeyBind(key,type)));
        }

       public static bool GetBool(string name, Type oType = typeof(bool))
       {
           return oType == typeof(KeyBind) ? Config.Item(name).GetValue<KeyBind>().Active : Config.Item(name).GetValue<bool>();
       }

        public static int GetValue(string name)
       {
           return Config.Item(name).GetValue<Slider>().Value;
       }

       public static int GetStringValue(string name)
       {
           return Config.Item(name).GetValue<StringList>().SelectedIndex;
       }


       public static bool ManaCheck(string name)
       {
           return Player.ManaPercent >= Config.Item(name).GetValue<Slider>().Value;
       }

        public static bool ItemReady(int id)
        {
            return Items.CanUseItem(id);
        }

        public static bool HasItem(int id)
        {
            return Items.HasItem(id);
        }
        

        public static bool HealthCheck(string name)
        {
            return Player.HealthPercent >= Config.Item(name).GetValue<Slider>().Value;
        }

        public static bool UseUnitItem(int item, Obj_AI_Hero target)
        {
            return Items.UseItem(item, target);
        }
       
        public static bool SelfCast(int item)
        {
            return Items.UseItem(item);
        }

        public static bool PlayerBuff(string name)
        {
            return Player.HasBuff(name);
        }

        public static void PotionCast(int id , string buff)
        {
            if (ItemReady(id) && !PlayerBuff(buff)
                && !Player.IsRecalling() && !Player.InFountain()
                && Player.CountEnemiesInRange(700) >= 1)
            {
                SelfCast(id);
            }
        }

        public static void ElixerCast(int id, string buff, string menuname)
        {
            if (Player.IsDead && Player.InShop()
                && Player.Gold >= 400 && !PlayerBuff(buff)
                && HasItem(id)
                && GetBool(menuname))
            {
                SelfCast(id);
            }
        }
    }
}
