using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Animations;

namespace Alex_s_unfortunate_journey
{
    public class Alex
    {
        private AnimatedSprite _idle;
        private AnimatedSprite _idle_left;
        private AnimatedSprite _walk;
        private AnimatedSprite _walk_left;
        private AnimatedSprite _death;
        private AnimatedSprite _death_left;
        private AnimatedSprite _jump;
        private AnimatedSprite _jump_left;

        public Alex(AnimatedSprite idle, AnimatedSprite idle_left, AnimatedSprite walk, AnimatedSprite walk_left, AnimatedSprite death, AnimatedSprite death_left, AnimatedSprite jump, AnimatedSprite jump_left)
        {
            _idle = idle;
            _idle_left = idle_left;
            _walk = walk;
            _walk_left = walk_left;
            _death = death;
            _death_left = death_left;
            _jump = jump;
            _jump_left = jump_left;
        }
    }
}
