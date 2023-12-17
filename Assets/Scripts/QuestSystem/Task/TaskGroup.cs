using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public enum TaskGroupState
{
	Inactive,
	Running,
	Complete
}

// ����Ʈ���� �����
[System.Serializable]
public class TaskGroup
{
	[SerializeField] private Task[] tasks;

	public IReadOnlyList<Task> Tasks => tasks;
	public Quest Owner { get; private set; }
	public bool IsAllTaskComplete => tasks.All(x => x.IsComplete);
	public bool IsComplete => State == TaskGroupState.Complete;
	public TaskGroupState State { get; private set; }

	// �ٸ� TaskGroupdmf �����ϴ� ������
	public TaskGroup(TaskGroup copyTarget)
	{
		tasks = copyTarget.Tasks.Select(x => Object.Instantiate(x)).ToArray();
	}

	public void Setup(Quest owner)
	{
		Owner = owner;
		foreach (var task in tasks)
			task.Setup(owner);
	}

	public void Start()
	{
		State = TaskGroupState.Running;
		foreach (var task in tasks)
			task.Start();
	}

	public void End()
	{
		State = TaskGroupState.Complete;
		foreach (var task in tasks)
			task.End();
	}

	public void ReceiveReport(string category, object target, int successCount)
	{
		foreach (var task in tasks)
		{
			if (task.IsTarget(category, target))
				task.ReceiveReport(successCount);
		}
	}

	public void Complete()
	{
		if (IsComplete)
			return;

		State = TaskGroupState.Complete;

		foreach (var task in tasks)
		{
			if (!task.IsComplete)
				task.Complete();
		}
	}

	// Ÿ���� ���� Task�� �ִ��� Ȯ����
	public Task FindTaskByTarget(object target) => tasks.FirstOrDefault(x => x.ContainsTarget(target));

	public Task FindTaskByTarget(TaskTarget target) => FindTaskByTarget(target.Value);
	public bool ContainsTarget(object target) => tasks.Any(x => x.ContainsTarget(target));
	public bool ContainsTarget(TaskTarget target) => ContainsTarget(target.Value);
}