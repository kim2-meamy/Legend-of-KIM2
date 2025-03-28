using UnityEngine;

public class AnimatorToHash
{ 
        public readonly int animMoveSpeed = Animator.StringToHash("moveSpeed");
        public readonly int animJump = Animator.StringToHash("Jump");
        public readonly int animGrounded = Animator.StringToHash("Grounded");
        public readonly int animDodging = Animator.StringToHash("Dodging");
        public readonly int animAttack = Animator.StringToHash("Attack");
        public readonly int animBossAttack = Animator.StringToHash("BossAttack");
        public readonly int animAttackEnd = Animator.StringToHash("AttackEnd");
        public readonly int animHit = Animator.StringToHash("Hit");
        public readonly int animDie = Animator.StringToHash("Hp");
        public readonly int animIsChase = Animator.StringToHash("isChase");
        public readonly int animIsDead = Animator.StringToHash("IsDead");
        public readonly int animHit1 = Animator.StringToHash("Hit1");
        public readonly int animHit2 = Animator.StringToHash("Hit2");
        public readonly int animStun = Animator.StringToHash("Stun");
        public readonly int animStunEnd = Animator.StringToHash("StunEnd");
}