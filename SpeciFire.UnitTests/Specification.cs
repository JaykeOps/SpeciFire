using System;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests
{
    public abstract class Specification<TSubject>
    {
        public static readonly IInitializationSpecification<TSubject> Initialize = new InitializationSpecification<TSubject>();

        public bool IsSatisfiedBySubject(TSubject subject)
        {
            Func<TSubject, bool> predicate = ToExpression().Compile();
            return predicate(subject);
        }

        public abstract Expression<Func<TSubject, bool>> ToExpression();
    }
}