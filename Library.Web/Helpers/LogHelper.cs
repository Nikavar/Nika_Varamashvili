using Library.Model.Models;
using Library.Service;
using Library.Web.Enums;
using Newtonsoft.Json;
using System.Reflection;

namespace Library.Web.HelperMethods
{
	public static class LogHelper
	{
		public static async Task AddEntityWithLog<TEntity>(TEntity entity, Func<TEntity, Task<TEntity>> AddEntityAsync, ILogService logService) where TEntity : class
		{
			var log = new LogInfo { TableName = typeof(TEntity).Name };

			try
			{
				entity = await AddEntityAsync(entity);
				var entityJson = JsonConvert.SerializeObject(entity, Formatting.Indented, new JsonSerializerSettings() 
										    { 
												ReferenceLoopHandling = ReferenceLoopHandling.Ignore 
											});

				log.LogContent = entityJson;
				log.LogStatus = LogStatus.Info.ToString();

				Type type = entity.GetType();

				if(type != null)
				{
					PropertyInfo idProperty =   type.GetProperty("id") ?? 
												type.GetProperty("ID") ?? 
												type.GetProperty("Id");

					if (idProperty != null)
					{
						log.EntityID = (int)idProperty.GetValue(entity);
					}
				}
			}

			catch (Exception ex)
			{
				log.LogContent = ex.Message;
				log.LogStatus = LogStatus.Error.ToString();
			}

			finally
			{
				await logService.AddLogAsync(log);
			}
		}
		public static async Task UpdateEntityWithLog<TEntity>(TEntity entity, Func<TEntity, Task> UpdateEntityAsync, ILogService logService) where TEntity : class
		{
			PropertyInfo idProperty = entity.GetType().GetProperty("id") ?? entity.GetType().GetProperty("ID")
							?? entity.GetType().GetProperty("Id");

			LogInfo log = new LogInfo();

			if (idProperty != null)
			{
				var id = (int)idProperty.GetValue(entity);
				log = await logService.GetLogByEntityId(id);
			}

			try
			{
				await UpdateEntityAsync(entity);
				var entityJson = JsonConvert.SerializeObject(entity, Formatting.Indented);
				log.LogContent = entityJson;
				log.LogStatus = LogStatus.Info.ToString();
			}

			catch (Exception ex)
			{
				log.LogContent = ex.Message;
				log.LogStatus = LogStatus.Error.ToString();
			}

			finally
			{
				await logService.UpdateLogAsync(log);
			}
		}
	}
}
