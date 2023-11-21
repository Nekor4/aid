﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Aid.UpdateManager
{
    public class AdvanceUpdateManager : MonoBehaviour
    {
        private List<UpdateEntity> _updateEntities = new();
        private List<UpdateEntity> _fixedUpdateEntities = new();
        private List<UpdateEntity> _lateUpdateEntities = new();

        private List<UpdateEntity> _updateEntitiesToRemove = new();
        private List<UpdateEntity> _fixedUpdateEntitiesToRemove = new();
        private List<UpdateEntity> _lateUpdateEntitiesToRemove = new();

        private static AdvanceUpdateManager _instance;

        private static AdvanceUpdateManager Instance
        {
            get
            {
                if (InstanceExists == false)
                {
                    _instance = new GameObject("AdvanceUpdateManager").AddComponent<AdvanceUpdateManager>();
                }

                return _instance;
            }
        }

        public static bool InstanceExists => _instance != null;

        public static IUpdateEntity StartUpdate(Action onUpdateAction, UpdateType updateType, int interval = 1)
        {
            return Instance.InternalStartUpdate(onUpdateAction, updateType, interval);
        }

        private IUpdateEntity InternalStartUpdate(Action onUpdateAction, UpdateType updateType, int interval = 1)
        {
            var updateEntity = new UpdateEntity(onUpdateAction, interval);
            GetListForUpdateType(updateType).Add(updateEntity);
            return updateEntity;
        }

        private List<UpdateEntity> GetListForUpdateType(UpdateType updateType)
        {
            switch (updateType)
            {
                case UpdateType.Update:
                    return _updateEntities;

                case UpdateType.FixedUpdate:
                    return _fixedUpdateEntities;

                case UpdateType.LateUpdate:
                    return _lateUpdateEntities;

                default:
                    throw new ArgumentOutOfRangeException(nameof(updateType), updateType, null);
            }
        }

        public static void Stop(IUpdateEntity updateEntity)
        {
            if (InstanceExists == false) return;

            Instance.InternalStop(updateEntity);
        }

        private void InternalStop(IUpdateEntity updateEntity)
        {
            var entity = updateEntity as UpdateEntity;
            Assert.IsNotNull(entity);

            if (_updateEntities.Contains(entity))
                _updateEntitiesToRemove.Add(entity);
            else if (_fixedUpdateEntities.Contains(entity))
                _fixedUpdateEntitiesToRemove.Add(entity);
            else if (_lateUpdateEntities.Contains(entity))
                _lateUpdateEntitiesToRemove.Add(entity);
        }

        private void Update()
        {
            SafelyRemoveEntities(_updateEntitiesToRemove, _updateEntities);

            for (int i = 0; i < _updateEntities.Count; i++)
            {
                _updateEntities[i].Tick();
            }
        }

        private void FixedUpdate()
        {
            SafelyRemoveEntities(_fixedUpdateEntitiesToRemove, _fixedUpdateEntities);

            for (int i = 0; i < _fixedUpdateEntities.Count; i++)
            {
                _fixedUpdateEntities[i].Tick();
            }
        }

        private void LateUpdate()
        {
            SafelyRemoveEntities(_lateUpdateEntitiesToRemove, _lateUpdateEntities);
            
            for (int i = 0; i < _lateUpdateEntities.Count; i++)
            {
                _lateUpdateEntities[i].Tick();
            }
        }

        private void SafelyRemoveEntities(List<UpdateEntity> entities, List<UpdateEntity> listFrom)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                listFrom.Remove(entities[i]);
            }

            entities.Clear();
        }
    }
}