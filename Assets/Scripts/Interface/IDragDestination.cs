﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Dragging
{
	/// <summary>
	/// DragItem을 드래그함
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IDragDestination<T> where T : class
    {
        /// <summary>
        /// How many of the given item can be accepted.
        /// </summary>
        /// <param name="item">The item type to potentially be added.</param>
        /// <returns>If there is no limit Int.MaxValue should be returned.</returns>
        int MaxAcceptable(T item);

        /// <summary>
        /// Update the UI and any data to reflect adding the item to this destination.
        /// </summary>
        /// <param name="item">The item type.</param>
        /// <param name="number">The quantity of items.</param>
        void AddItems(T item, int number);
    }
}