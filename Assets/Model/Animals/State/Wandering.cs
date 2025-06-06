﻿using Safari.Model.Animals.Movement;
using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.Model.Animals.State
{
    public class Wandering : Animals.State.State
    {
        public MovementCommand? Key => key;

        private MovementCommand? key;

        public Wandering(Animal owner, double hydrationPercent, double saturationPercent, double breedingCooldown) : base(owner, hydrationPercent, saturationPercent, breedingCooldown)
        {
        }

        public override void OnEnter()
        {
            Debug.Log("Wandering started");
            Animal? groupMember = AlreadyWandering();
            base.OnEnter();
            WanderingMovementCommand command;
            if (groupMember?.Movement?.CurrentCommand is WanderingMovementCommand followed)
            {
                command = new WanderingMovementCommand(followed);
                key = followed;
            }
            else
            {
                command = new WanderingMovementCommand();
                key = command;
            }
            command.Finished += OnWanderingFinished;
            owner.Movement.ExecuteMovement(command);
        }
        public override void Update(float deltaTime, int speedFactor)
        {
            base.Update(deltaTime, speedFactor);
            AllowSearchingWater();
            AllowSearchingFood();
            AllowSearchingMate();
        }

        public override bool TransitionToMateAllowed()
        {
            return true;
        }

        protected override Func<double, double, double, State> ReturnToIfCantFindMate()
        {
            return (h, s, b) => new Wandering(owner, h, s, b);
        }

        private void OnWanderingFinished(object sender, EventArgs e)
        {
            Debug.Log("Wandering finished");
            TransitionTo(new WaitingForOthers(owner, hydrationPercent, saturationPercent, breedingCooldown, key));
        }

        private Animal? AlreadyWandering()
        {
            foreach (var animal in owner.Group.Animals)
            {
                if (animal != owner && animal.State is Wandering)
                {
                    return animal;
                }
            }
            return null;
        }

    }
}
