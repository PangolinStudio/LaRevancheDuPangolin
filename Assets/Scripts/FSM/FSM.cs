using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM {

	public delegate void State();
	public float timer;
	
	private List<State> stack;

	public FSM(){
		stack = new List<State>();
		timer = 0;
	}

	public void Add(State state) {
		if (GetActiveState() != state) {
			timer = 0;
			stack.Add(state);
		}
    }

	public State Pop() {
		if (stack.Count > 0) {
			State last = stack[stack.Count - 1];
			stack.RemoveAt(stack.Count - 1);
			timer = 0;
			return last;
		}
		return null;
	}

	public bool Remove(State state) {
		return stack.Remove(state);
	}

	public State GetActiveState()
    {
        if (stack.Count > 0)
            return stack[stack.Count - 1];
        else
            return null;
    }
 
    public void Update(float elapsed) {
        State activeState = GetActiveState();
        if (activeState != null) 
        {
            activeState();
        }
		timer += elapsed;
    }
}
