﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShardingCore.Exceptions;
using ShardingCore.Sharding.Enumerators;
using ShardingCore.Sharding.MergeEngines.Abstractions;

namespace ShardingCore.Sharding.MergeEngines.EnumeratorStreamMergeEngines.StreamMergeCombines
{
    internal class SingleStreamMergeCombine:IStreamMergeCombine
    {
        public IStreamMergeAsyncEnumerator<TEntity> StreamMergeEnumeratorCombine<TEntity>(StreamMergeContext streamMergeContext,
            IStreamMergeAsyncEnumerator<TEntity>[] streamsAsyncEnumerators)
        {
            if (streamsAsyncEnumerators.Length != 1)
                throw new ShardingCoreInvalidOperationException($"single query has more {nameof(IStreamMergeAsyncEnumerator<TEntity>)}");
            return streamsAsyncEnumerators[0];
        }
    }
}
