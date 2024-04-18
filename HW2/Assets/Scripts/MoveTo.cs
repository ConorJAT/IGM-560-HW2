using System;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable]
    public class MoveTo : Task
    {
        // The character that is moving to a different location.
        private Character mover;
        public Character Mover
        {
            get { return mover; }
            set { mover = value; }
        }

        // The location the character is moving to.
        private Location where;
        public Location Where
        {
            get { return where; }
            set { where = value; }
        }

        // This constructor defaults the character and location to None.
        public MoveTo() : this(Character.None, Location.None) { }

        // This constructor takes parameters for the character and location.
        public MoveTo (Character mover, Location where)
        {
            Mover = mover;
            Where = where;
        }

        // This method runs the MoveTo action on the given WorldState object.
        public override bool run (WorldState state)
        {
            // Fill in your conditional logic here.

            // Is the char in the right place?
            if (state.CharacterPosition[Mover] != Where)
            {
                // Is the target place connected to the char's current place?
                if (state.ConnectedLocations[state.CharacterPosition[Mover]].Contains(Where))
                {
                    // Is there a door between where the char needs to go?
                    if (state.BetweenLocations.ContainsKey(state.CharacterPosition[Mover]) && state.BetweenLocations[state.CharacterPosition[Mover]].Item1.Equals(Thing.Gate))
                    {
                        // Is the door open?
                        if (state.Open[state.BetweenLocations[state.CharacterPosition[Mover]].Item1])
                        {
                            state.CharacterPosition[Mover] = Where;
                            if (state.Debug) Debug.Log(this + " Success");
                            return true;
                        }
                        else
                        {
                            if (state.Debug) Debug.Log(this + " Fail");
                            return false;
                        }
                    }

                    state.CharacterPosition[Mover] = Where;
                    if (state.Debug) Debug.Log(this + " Success");
                    return true;
                }
            }

            if (state.Debug) Debug.Log(this + " Fail");
            return false;
        }

        // Creates and returns a string describing the MoveTo action.
        public override string ToString()
        {
            return mover + " moves to " + where;
        }
    }
}
