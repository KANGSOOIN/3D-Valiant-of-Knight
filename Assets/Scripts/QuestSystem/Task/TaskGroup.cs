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

// 퀘스트에서 사용함
[System.Serializable]
public class TaskGroup
{
	[SerializeField] private Task[] tasks;

	public IReadOnlyList<Task> Tasks => tasks;
	public Quest Owner { get; private set; }
	public bool IsAllTaskComplete => tasks.All(x => x.IsComplete);
	public bool IsComplete => State == TaskGroupState.Complete;
	public TaskGroupState State { get; private set; }

	// 다른 TaskGroupdmf 복사하는 생성자
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

	// 타켓을 가진 Task가 있는지 확인함
	public Task FindTaskByTarget(object target) => tasks.FirstOrDefault(x => x.ContainsTarget(target));

	public Task FindTaskByTarget(TaskTarget target) => FindTaskByTarget(target.Value);
	public bool ContainsTarget(object target) => tasks.Any(x => x.ContainsTarget(target));
	public bool ContainsTarget(TaskTarget target) => ContainsTarget(target.Value);
}
