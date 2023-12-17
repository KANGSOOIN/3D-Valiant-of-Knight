using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

// ����Ʈ ���� ����
public enum TaskState
{
	Inactive,
	Running,
	Complete
}

[CreateAssetMenu(menuName = "Quest/Task/Task", fileName = "Task_")]
public class Task : ScriptableObject
{
	#region Events
	public delegate void StateChangedHandler(Task task, TaskState currentState, TaskState prevState);
	public delegate void SuccessChangedHandler(Task task, int currentSuccess, int prevSuccess);
	#endregion

	[SerializeField] private Category category;

	[Header("Text")]
	[SerializeField] public string codeName; // �ڵ� �̸�
	[SerializeField] private string description; // ����

	[Header("Target")]
	[SerializeField] private TaskTarget[] targets; // Ÿ���� ���� ���� ��� ���

	[Header("Action")]
	[SerializeField] private TaskAction action;

	[Header("Setting")]
	[SerializeField] private InitialSuccessValue initialSuccessValue;
	[SerializeField] private int needSuccessToComplete; // �ʿ��� ���� Ƚ��
	[SerializeField] private bool canReceiveReportsDuringCompletion; // ����Ʈ �Ϸ��߾ ��� ���� Ƚ���� Ȯ����

	private TaskState state;
	private int currentSuccess;

	public event StateChangedHandler onStateChanged;
	public event SuccessChangedHandler onSuccessChanged;

	// ������ Ƚ��
	public int CurrentSuccess 
	{ 
		get => currentSuccess;
		set
		{
			int prevSuccess = currentSuccess;
			currentSuccess = Mathf.Clamp(value, 0, needSuccessToComplete);
			if (currentSuccess != prevSuccess)
			{
				State = currentSuccess == needSuccessToComplete ? TaskState.Complete : TaskState.Running;
				onSuccessChanged?.Invoke(this, currentSuccess, prevSuccess);
			}
		}
	}
	public Category Category => category;
	public string CodeName => codeName;
	public string Description => description;
	public int NeedSuccessToComplete => needSuccessToComplete;

	public TaskState State
	{
		get => state;
		set
		{
			var prevState = state;
			state = value;
			onStateChanged?.Invoke(this, state, prevState);
		}

	}
	public bool IsComplete => State == TaskState.Complete;
	public Quest Owner { get; private set; }

	// Awake ����
	public void Setup(Quest owner)
	{
		Owner = owner;
	}

	public void Start()
	{
		State = TaskState.Running;
		if (initialSuccessValue)
			CurrentSuccess = initialSuccessValue.GetValue(this);
	}

	public void End()
	{
		onStateChanged = null;
		onSuccessChanged = null;
	}

	// �ܺο��� CurrentSuccess ���� ������ �� ����
	public void ReceiveReport(int successCount)
	{
		CurrentSuccess = action.Run(this, CurrentSuccess, successCount);
	}

	public void Complete()
	{
		CurrentSuccess = needSuccessToComplete;
	}

	// Ÿ������ Ȯ����
	public bool IsTarget(string category, object target)
		=> Category == category &&
		targets.Any(x => x.IsEqual(target)) &&
		(!IsComplete || (IsComplete && canReceiveReportsDuringCompletion));

	public bool ContainsTarget(object target) => targets.Any(x => x.IsEqual(target));
}


