﻿using Pole.Sagas.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pole.Sagas.Core
{
    public interface ISagaStorage
    {
        Task SagaStarted(string sagaId, string serviceName,DateTime addTime);
        Task SagaEnded(string sagaId, DateTime ExpiresAt);
        Task ActivityExecuteStarted(string activityId, string sagaId, int timeOutSeconds, byte[] ParameterData, int order,DateTime addTime);
        Task ActivityRetried(string activityId, string status, int retries, ActivityRetryType retryType);
        Task ActivityExecuteAborted(string activityId, string errors);
        Task ActivityCompensateAborted(string activityId, string sagaId, string errors);
        Task ActivityEnded(string activityId, byte[] resultData);
        Task ActivityCompensated(string activityId);
        Task ActivityExecuteOvertime(string activityId, string sagaId, string errors);
        Task ActivityRevoked(string activityId);
    }
}