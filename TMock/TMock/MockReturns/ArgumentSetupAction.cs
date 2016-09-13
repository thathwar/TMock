namespace TMock.MockReturns
{
    internal class ArgumentSetupAction : IArgumentSetupAction
    {
        private readonly ExpectedArgument _argument;
        public ArgumentSetupAction(ExpectedArgument argument)
        {
            _argument = argument;
        }

        public IMethodTestSetupAction SetArguments(object arg)
        {
            _argument.ParamSetValue = arg;
           return new MethodTestSetupAction(_argument);
        }
    }
}
