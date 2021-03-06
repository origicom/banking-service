﻿using System;

namespace Base.Types
{
    public class ValidatorSpecifier
    {
        public readonly ValidatorGuid Id;
        public readonly int Version;

        public ValidatorSpecifier(ValidatorGuid validatorId, int version)
        {
            if(validatorId == null)
            {
                throw new ArgumentNullException($"{nameof(validatorId)} cannot be null.");
            }

            if(version <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(version)} cannot be zero or negative.");
            }

            Id = validatorId;
            Version = version;
        }
    }
}
