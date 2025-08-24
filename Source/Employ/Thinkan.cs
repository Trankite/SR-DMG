using SR_DMG.Source.Example;

namespace SR_DMG.Source.Employ
{
    public class Thinkan
    {
        public List<Role> Roles { set; get; } = [];
        public List<Equip> Equips { set; get; } = [];
        public List<Relic> Relics { set; get; } = [];
        public List<Enemy> Enemies { set; get; } = [];
        public interface ISkill
        {
            List<Skill> Skills { set; get; }
        }

    }
}